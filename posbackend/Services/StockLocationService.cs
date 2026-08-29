using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using posbackend.Data;
using posbackend.DTOs;
using posbackend.Models;

namespace posbackend.Services
{
    public class StockLocationService : IStockLocationService
    {
        private readonly DapperContext _dapperContext;
        private readonly AppDbContext _efContext;

        public StockLocationService(DapperContext dapperContext, AppDbContext efContext)
        {
            _dapperContext = dapperContext;
            _efContext = efContext;
        }

        #region SELECT Operations (Dapper)

        public async Task<IEnumerable<StockLocationDto>> GetStockLocationsAsync(StockLocationQueryParameters queryParams)
        {
            using var connection = _dapperContext.CreateConnection();

            var whereClauses = new List<string>();
            var parameters = new DynamicParameters();

            if (queryParams.TenantId.HasValue && queryParams.TenantId.Value != Guid.Empty)
            {
                whereClauses.Add("tenant_id = @TenantId");
                parameters.Add("TenantId", queryParams.TenantId.Value);
            }

            if (queryParams.StoreId.HasValue && queryParams.StoreId.Value != Guid.Empty)
            {
                whereClauses.Add("store_id = @StoreId");
                parameters.Add("StoreId", queryParams.StoreId.Value);
            }

            string whereSql = whereClauses.Any() ? "WHERE " + string.Join(" AND ", whereClauses) : "";

            string sql = $@"
                SELECT 
                    id AS Id,
                    tenant_id AS TenantId,
                    store_id AS StoreId,
                    name AS Name,
                    is_default AS IsDefault,
                    is_active AS IsActive,
                    created_at AS CreatedAt
                FROM stock_locations
                {whereSql}
                ORDER BY is_default DESC, name ASC";

            return await connection.QueryAsync<StockLocationDto>(sql, parameters);
        }

        public async Task<StockLocationDto?> GetStockLocationByIdAsync(Guid id)
        {
            using var connection = _dapperContext.CreateConnection();

            string sql = @"
                SELECT 
                    id AS Id,
                    tenant_id AS TenantId,
                    store_id AS StoreId,
                    name AS Name,
                    is_default AS IsDefault,
                    is_active AS IsActive,
                    created_at AS CreatedAt
                FROM stock_locations
                WHERE id = @Id";

            return await connection.QueryFirstOrDefaultAsync<StockLocationDto>(sql, new { Id = id });
        }

        #endregion

        #region MUTATION Operations (Entity Framework Core)

        public async Task<StockLocationDto> CreateStockLocationAsync(CreateStockLocationDto dto)
        {
            // If setting as default, unset other defaults in the same store
            if (dto.IsDefault)
            {
                var existingDefaults = await _efContext.StockLocations
                    .Where(sl => sl.StoreId == dto.StoreId && sl.IsDefault)
                    .ToListAsync();
                foreach (var loc in existingDefaults)
                {
                    loc.IsDefault = false;
                }
            }

            var location = new StockLocation
            {
                Id = Guid.NewGuid(),
                TenantId = dto.TenantId != Guid.Empty ? dto.TenantId : Guid.NewGuid(),
                StoreId = dto.StoreId != Guid.Empty ? dto.StoreId : Guid.NewGuid(),
                Name = dto.Name,
                IsDefault = dto.IsDefault,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _efContext.StockLocations.Add(location);
            await _efContext.SaveChangesAsync();

            return new StockLocationDto
            {
                Id = location.Id,
                TenantId = location.TenantId,
                StoreId = location.StoreId,
                Name = location.Name,
                IsDefault = location.IsDefault,
                IsActive = location.IsActive,
                CreatedAt = location.CreatedAt
            };
        }

        public async Task<StockLocationDto?> UpdateStockLocationAsync(Guid id, UpdateStockLocationDto dto)
        {
            var location = await _efContext.StockLocations.FirstOrDefaultAsync(sl => sl.Id == id);
            if (location == null)
            {
                return null;
            }

            // If setting as default, unset other defaults in the same store
            if (dto.IsDefault && !location.IsDefault)
            {
                var existingDefaults = await _efContext.StockLocations
                    .Where(sl => sl.StoreId == location.StoreId && sl.Id != id && sl.IsDefault)
                    .ToListAsync();
                foreach (var loc in existingDefaults)
                {
                    loc.IsDefault = false;
                }
            }

            location.Name = dto.Name;
            location.IsDefault = dto.IsDefault;
            location.IsActive = dto.IsActive;

            await _efContext.SaveChangesAsync();

            return new StockLocationDto
            {
                Id = location.Id,
                TenantId = location.TenantId,
                StoreId = location.StoreId,
                Name = location.Name,
                IsDefault = location.IsDefault,
                IsActive = location.IsActive,
                CreatedAt = location.CreatedAt
            };
        }

        public async Task<bool> DeleteStockLocationAsync(Guid id)
        {
            var location = await _efContext.StockLocations.FirstOrDefaultAsync(sl => sl.Id == id);
            if (location == null)
            {
                return false;
            }

            _efContext.StockLocations.Remove(location);
            await _efContext.SaveChangesAsync();
            return true;
        }

        #endregion
    }
}
