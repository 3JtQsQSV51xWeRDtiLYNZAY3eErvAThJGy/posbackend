using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using posbackend.Data;
using posbackend.DTOs;

namespace posbackend.Services
{
    public class ItemTypeService : IItemTypeService
    {
        private readonly DapperContext _dapperContext;

        public ItemTypeService(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<PagedResultDto<ItemTypeDto>> GetItemTypesAsync(ItemTypeQueryParameters queryParams)
        {
            using var connection = _dapperContext.CreateConnection();

            var whereClauses = new List<string>();
            var parameters = new DynamicParameters();

            if (queryParams.TenantId.HasValue && queryParams.TenantId.Value != Guid.Empty)
            {
                whereClauses.Add("tenant_id = @TenantId");
                parameters.Add("TenantId", queryParams.TenantId.Value);
            }

            if (!string.IsNullOrWhiteSpace(queryParams.Search))
            {
                whereClauses.Add("(code LIKE @Search OR name LIKE @Search OR description LIKE @Search)");
                parameters.Add("Search", $"%{queryParams.Search}%");
            }

            if (queryParams.IsActive.HasValue)
            {
                whereClauses.Add("is_active = @IsActive");
                parameters.Add("IsActive", queryParams.IsActive.Value);
            }

            if (queryParams.IsService.HasValue)
            {
                whereClauses.Add("is_service = @IsService");
                parameters.Add("IsService", queryParams.IsService.Value);
            }

            string whereSql = whereClauses.Any() ? "WHERE " + string.Join(" AND ", whereClauses) : "";

            string countSql = $"SELECT COUNT(1) FROM item_types {whereSql}";
            int totalCount = await connection.ExecuteScalarAsync<int>(countSql, parameters);

            int page = queryParams.Page < 1 ? 1 : queryParams.Page;
            int limit = queryParams.Limit < 1 ? 10 : queryParams.Limit;
            int offset = (page - 1) * limit;

            parameters.Add("Limit", limit);
            parameters.Add("Offset", offset);

            string querySql = $@"
                SELECT 
                    id AS Id,
                    tenant_id AS TenantId,
                    code AS Code,
                    name AS Name,
                    description AS Description,
                    track_stock_default AS TrackStockDefault,
                    is_service AS IsService,
                    is_active AS IsActive,
                    created_at AS CreatedAt,
                    updated_at AS UpdatedAt
                FROM item_types
                {whereSql}
                ORDER BY id ASC
                LIMIT @Limit OFFSET @Offset";

            var items = (await connection.QueryAsync<ItemTypeDto>(querySql, parameters)).ToList();

            return new PagedResultDto<ItemTypeDto>
            {
                TotalCount = totalCount,
                Page = page,
                Limit = limit,
                Data = items
            };
        }

        public async Task<ItemTypeDto?> GetItemTypeByIdAsync(int id)
        {
            using var connection = _dapperContext.CreateConnection();

            string querySql = @"
                SELECT 
                    id AS Id,
                    tenant_id AS TenantId,
                    code AS Code,
                    name AS Name,
                    description AS Description,
                    track_stock_default AS TrackStockDefault,
                    is_service AS IsService,
                    is_active AS IsActive,
                    created_at AS CreatedAt,
                    updated_at AS UpdatedAt
                FROM item_types
                WHERE id = @Id";

            return await connection.QueryFirstOrDefaultAsync<ItemTypeDto>(querySql, new { Id = id });
        }
    }
}
