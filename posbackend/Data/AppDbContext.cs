using Microsoft.EntityFrameworkCore;
using posbackend.Models;

namespace posbackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<StockLocation> StockLocations { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.TenantId).HasColumnName("tenant_id");
                entity.Property(e => e.CategoryId).HasColumnName("category_id");
                entity.Property(e => e.Name).HasColumnName("name").IsRequired();
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.ItemType).HasColumnName("item_type");
                entity.Property(e => e.ItemTypeId).HasColumnName("item_type_id");
                entity.Property(e => e.TrackStock).HasColumnName("track_stock");
                entity.Property(e => e.IsPurchaseable).HasColumnName("is_purchaseable");
                entity.Property(e => e.DurationMinutes).HasColumnName("duration_minutes");
                entity.Property(e => e.IsActive).HasColumnName("is_active");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
                entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            });

            modelBuilder.Entity<ProductVariant>(entity =>
            {
                entity.ToTable("product_variants");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.Sku).HasColumnName("sku");
                entity.Property(e => e.Barcode).HasColumnName("barcode");
                entity.Property(e => e.CostPrice).HasColumnName("cost_price").HasColumnType("decimal(18,2)");
                entity.Property(e => e.SellPrice).HasColumnName("sell_price").HasColumnType("decimal(18,2)");
                entity.Property(e => e.Attributes).HasColumnName("attributes");
                entity.Property(e => e.IsActive).HasColumnName("is_active");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.TenantId).HasColumnName("tenant_id");
                entity.Property(e => e.ParentId).HasColumnName("parent_id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.SortOrder).HasColumnName("sort_order");
                entity.Property(e => e.IsActive).HasColumnName("is_active");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            });

            modelBuilder.Entity<ItemType>(entity =>
            {
                entity.ToTable("item_types");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id"); // serial4 (int)
                entity.Property(e => e.TenantId).HasColumnName("tenant_id");
                entity.Property(e => e.Code).HasColumnName("code");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.TrackStockDefault).HasColumnName("track_stock_default");
                entity.Property(e => e.IsService).HasColumnName("is_service");
                entity.Property(e => e.IsActive).HasColumnName("is_active");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            });

            modelBuilder.Entity<StockLocation>(entity =>
            {
                entity.ToTable("stock_locations");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.TenantId).HasColumnName("tenant_id");
                entity.Property(e => e.StoreId).HasColumnName("store_id");
                entity.Property(e => e.Name).HasColumnName("name").IsRequired();
                entity.Property(e => e.IsDefault).HasColumnName("is_default");
                entity.Property(e => e.IsActive).HasColumnName("is_active");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.TenantId).HasColumnName("tenant_id");
                entity.Property(e => e.StoreId).HasColumnName("store_id");
                entity.Property(e => e.RoleId).HasColumnName("role_id");
                entity.Property(e => e.Username).HasColumnName("username").IsRequired();
                entity.Property(e => e.Email).HasColumnName("email").IsRequired();
                entity.Property(e => e.PasswordHash).HasColumnName("password_hash").IsRequired();
                entity.Property(e => e.FirstName).HasColumnName("first_name");
                entity.Property(e => e.LastName).HasColumnName("last_name");
                entity.Property(e => e.Phone).HasColumnName("phone");
                entity.Property(e => e.IsActive).HasColumnName("is_active");
                entity.Property(e => e.LastLoginAt).HasColumnName("last_login_at");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
                entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            });
        }
    }
}
