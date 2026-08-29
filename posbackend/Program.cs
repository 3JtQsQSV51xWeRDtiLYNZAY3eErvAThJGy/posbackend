using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using posbackend.Data;
using posbackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                      ?? "Host=localhost;Database=posdb;Username=postgres;Password=postgres";

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var provider = builder.Configuration["DatabaseProvider"] ?? "PostgreSQL";
    if (provider.Equals("SqlServer", StringComparison.OrdinalIgnoreCase))
    {
        options.UseSqlServer(connectionString);
    }
    else
    {
        options.UseNpgsql(connectionString);
    }
});

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IItemTypeService, ItemTypeService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IStockLocationService, StockLocationService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Configure JWT Authentication
var jwtSecret = builder.Configuration["Jwt:Secret"] ?? "POS_Backend_Super_Secret_JWT_Key_2026_With_Minimum_256_Bits!";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "POSBackendApi";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "POSBackendClient";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
        ValidateIssuer = true,
        ValidIssuer = jwtIssuer,
        ValidateAudience = true,
        ValidAudience = jwtAudience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddControllers();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "POS Backend API",
        Version = "v1",
        Description = "API for POS Backend System with JWT Authentication (UUID Primary Keys)"
    });

    // Add JWT Bearer Security Definition in Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "กรุณาใส่ JWT Token ในรูปแบบ: Bearer <your_token>"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddOpenApi();

var app = builder.Build();

// Ensure database schema matches UUID specifications
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        // 1. Enable pgcrypto extension for gen_random_uuid()
        context.Database.ExecuteSqlRaw(@"CREATE EXTENSION IF NOT EXISTS ""pgcrypto"";");

        // 2. Check if users table or products table has integer id column and needs migration to UUID
        var needsUuidMigration = false;
        using (var command = context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "SELECT COUNT(*) FROM information_schema.columns WHERE table_name = 'users' AND column_name = 'id' AND data_type = 'integer';";
            context.Database.OpenConnection();
            var count = Convert.ToInt32(command.ExecuteScalar());
            if (count > 0)
            {
                needsUuidMigration = true;
            }
        }

        if (needsUuidMigration)
        {
            Console.WriteLine("[Database Init] Migrating database tables to UUID primary keys...");
            context.Database.ExecuteSqlRaw(@"
                DROP TABLE IF EXISTS product_variants CASCADE;
                DROP TABLE IF EXISTS products CASCADE;
                DROP TABLE IF EXISTS categories CASCADE;
                DROP TABLE IF EXISTS stock_locations CASCADE;
                DROP TABLE IF EXISTS users CASCADE;
            ");
        }

        // 3. Create/Ensure tables with correct UUID and SERIAL types
        context.Database.ExecuteSqlRaw(@"
            -- Users (UUID)
            CREATE TABLE IF NOT EXISTS users (
                id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                tenant_id UUID NOT NULL DEFAULT gen_random_uuid(),
                store_id UUID NULL,
                role_id UUID NOT NULL DEFAULT gen_random_uuid(),
                username VARCHAR(100) NOT NULL,
                email VARCHAR(255) NOT NULL,
                password_hash VARCHAR(255) NOT NULL,
                first_name VARCHAR(100) NULL,
                last_name VARCHAR(100) NULL,
                phone VARCHAR(50) NULL,
                is_active BOOLEAN NOT NULL DEFAULT TRUE,
                last_login_at TIMESTAMP NULL,
                created_at TIMESTAMP NOT NULL DEFAULT NOW(),
                updated_at TIMESTAMP NULL,
                deleted_at TIMESTAMP NULL
            );

            -- Categories (UUID)
            CREATE TABLE IF NOT EXISTS categories (
                id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                tenant_id UUID NOT NULL DEFAULT gen_random_uuid(),
                parent_id UUID NULL,
                name VARCHAR(255) NOT NULL,
                sort_order INT NOT NULL DEFAULT 0,
                is_active BOOLEAN NOT NULL DEFAULT TRUE,
                created_at TIMESTAMP NOT NULL DEFAULT NOW(),
                updated_at TIMESTAMP NULL
            );

            -- Products (UUID)
            CREATE TABLE IF NOT EXISTS products (
                id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                tenant_id UUID NOT NULL DEFAULT gen_random_uuid(),
                category_id UUID NULL,
                name VARCHAR(255) NOT NULL,
                description TEXT NULL,
                item_type VARCHAR(50) NOT NULL DEFAULT 'PHYSICAL',
                item_type_id INT NULL,
                track_stock BOOLEAN NOT NULL DEFAULT TRUE,
                is_purchaseable BOOLEAN NOT NULL DEFAULT TRUE,
                duration_minutes INT NULL,
                is_active BOOLEAN NOT NULL DEFAULT TRUE,
                created_at TIMESTAMP NOT NULL DEFAULT NOW(),
                updated_at TIMESTAMP NULL,
                deleted_at TIMESTAMP NULL
            );

            -- Product Variants (UUID)
            CREATE TABLE IF NOT EXISTS product_variants (
                id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                product_id UUID NOT NULL,
                sku VARCHAR(100) NULL,
                barcode VARCHAR(100) NULL,
                cost_price DECIMAL(18,2) NOT NULL DEFAULT 0,
                sell_price DECIMAL(18,2) NOT NULL DEFAULT 0,
                attributes TEXT NULL,
                is_active BOOLEAN NOT NULL DEFAULT TRUE,
                created_at TIMESTAMP NOT NULL DEFAULT NOW()
            );

            -- Stock Locations (UUID)
            CREATE TABLE IF NOT EXISTS stock_locations (
                id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                tenant_id UUID NOT NULL DEFAULT gen_random_uuid(),
                store_id UUID NOT NULL DEFAULT gen_random_uuid(),
                name VARCHAR(255) NOT NULL,
                is_default BOOLEAN NOT NULL DEFAULT FALSE,
                is_active BOOLEAN NOT NULL DEFAULT TRUE,
                created_at TIMESTAMP NOT NULL DEFAULT NOW()
            );

            -- Item Types (Exception: SERIAL / serial4)
            CREATE TABLE IF NOT EXISTS item_types (
                id SERIAL PRIMARY KEY,
                tenant_id UUID NOT NULL DEFAULT gen_random_uuid(),
                code VARCHAR(50) NOT NULL,
                name VARCHAR(100) NOT NULL,
                description TEXT NULL,
                track_stock_default BOOLEAN NOT NULL DEFAULT TRUE,
                is_service BOOLEAN NOT NULL DEFAULT FALSE,
                is_active BOOLEAN NOT NULL DEFAULT TRUE,
                created_at TIMESTAMP NOT NULL DEFAULT NOW(),
                updated_at TIMESTAMP NULL
            );

            -- Tenant Members (Exception: SERIAL / serial4)
            CREATE TABLE IF NOT EXISTS tenant_members (
                id SERIAL PRIMARY KEY,
                tenant_id UUID NOT NULL,
                user_id UUID NOT NULL,
                role_id UUID NOT NULL,
                is_active BOOLEAN NOT NULL DEFAULT TRUE,
                created_at TIMESTAMP NOT NULL DEFAULT NOW()
            );

            -- User Stores (Exception: SERIAL / serial4)
            CREATE TABLE IF NOT EXISTS user_stores (
                id SERIAL PRIMARY KEY,
                user_id UUID NOT NULL,
                store_id UUID NOT NULL,
                is_active BOOLEAN NOT NULL DEFAULT TRUE,
                created_at TIMESTAMP NOT NULL DEFAULT NOW()
            );
        ");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[Database Init] Warning ensuring database schema: {ex.Message}");
    }
}

// Configure Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "POS Backend API v1");
    c.RoutePrefix = string.Empty; // Launch Swagger UI at root URL
});

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
