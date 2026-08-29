using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using posbackend.Data;
using posbackend.DTOs;
using posbackend.Models;

namespace posbackend.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            // Check if username or email already exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username.ToLower() == dto.Username.ToLower() || u.Email.ToLower() == dto.Email.ToLower());

            if (existingUser != null)
            {
                if (existingUser.Username.Equals(dto.Username, StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException("Username already exists.");
                }
                throw new InvalidOperationException("Email already exists.");
            }

            // Hash password using BCrypt
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                TenantId = dto.TenantId != Guid.Empty ? dto.TenantId : Guid.NewGuid(),
                StoreId = dto.StoreId,
                RoleId = dto.RoleId != Guid.Empty ? dto.RoleId : Guid.NewGuid(),
                Username = dto.Username.Trim(),
                Email = dto.Email.Trim().ToLower(),
                PasswordHash = passwordHash,
                FirstName = dto.FirstName ?? string.Empty,
                LastName = dto.LastName ?? string.Empty,
                Phone = dto.Phone ?? string.Empty,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return GenerateAuthResponse(newUser);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => (u.Username.ToLower() == dto.UsernameOrEmail.ToLower() || u.Email.ToLower() == dto.UsernameOrEmail.ToLower()) && u.DeletedAt == null);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username/email or password.");
            }

            if (!user.IsActive)
            {
                throw new UnauthorizedAccessException("Account is disabled. Please contact system administrator.");
            }

            // Verify password
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Invalid username/email or password.");
            }

            // Update last login timestamp
            user.LastLoginAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return GenerateAuthResponse(user);
        }

        public async Task<UserProfileDto?> GetUserProfileByIdAsync(Guid userId)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId && u.DeletedAt == null);

            if (user == null) return null;

            return MapToUserProfileDto(user);
        }

        private AuthResponseDto GenerateAuthResponse(User user)
        {
            var jwtSecret = _configuration["Jwt:Secret"] ?? "POS_Backend_Super_Secret_JWT_Key_2026_With_Minimum_256_Bits!";
            var jwtIssuer = _configuration["Jwt:Issuer"] ?? "POSBackendApi";
            var jwtAudience = _configuration["Jwt:Audience"] ?? "POSBackendClient";
            var expireMinutesStr = _configuration["Jwt:ExpireMinutes"] ?? "1440";
            double expireMinutes = double.TryParse(expireMinutesStr, out var mins) ? mins : 1440;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("tenant_id", user.TenantId.ToString()),
                new Claim("store_id", user.StoreId?.ToString() ?? string.Empty),
                new Claim("role_id", user.RoleId.ToString())
            };

            var expiresAt = DateTime.UtcNow.AddMinutes(expireMinutes);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthResponseDto
            {
                Token = tokenString,
                ExpiresAt = expiresAt,
                User = MapToUserProfileDto(user)
            };
        }

        private static UserProfileDto MapToUserProfileDto(User user)
        {
            return new UserProfileDto
            {
                Id = user.Id,
                TenantId = user.TenantId,
                StoreId = user.StoreId,
                RoleId = user.RoleId,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
