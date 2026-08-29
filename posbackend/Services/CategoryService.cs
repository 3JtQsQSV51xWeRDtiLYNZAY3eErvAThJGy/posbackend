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
    public class CategoryService : ICategoryService
    {
        private readonly DapperContext _dapperContext;
        private readonly AppDbContext _efContext;

        public CategoryService(DapperContext dapperContext, AppDbContext efContext)
        {
            _dapperContext = dapperContext;
            _efContext = efContext;
        }

        #region SELECT Operations (Dapper)

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync(CategoryQueryParameters queryParams)
        {
            using var connection = _dapperContext.CreateConnection();

            var whereClauses = new List<string>();
            var parameters = new DynamicParameters();

            if (queryParams.TenantId.HasValue && queryParams.TenantId.Value != Guid.Empty)
            {
                whereClauses.Add("tenant_id = @TenantId");
                parameters.Add("TenantId", queryParams.TenantId.Value);
            }

            if (queryParams.ParentId.HasValue && queryParams.ParentId.Value != Guid.Empty)
            {
                whereClauses.Add("parent_id = @ParentId");
                parameters.Add("ParentId", queryParams.ParentId.Value);
            }

            string whereSql = whereClauses.Any() ? "WHERE " + string.Join(" AND ", whereClauses) : "";

            string sql = $@"
                SELECT 
                    id AS Id,
                    tenant_id AS TenantId,
                    parent_id AS ParentId,
                    name AS Name,
                    sort_order AS SortOrder,
                    is_active AS IsActive,
                    created_at AS CreatedAt,
                    updated_at AS UpdatedAt
                FROM categories
                {whereSql}
                ORDER BY sort_order ASC, name ASC";

            return await connection.QueryAsync<CategoryDto>(sql, parameters);
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(Guid id)
        {
            using var connection = _dapperContext.CreateConnection();

            string sql = @"
                SELECT 
                    id AS Id,
                    tenant_id AS TenantId,
                    parent_id AS ParentId,
                    name AS Name,
                    sort_order AS SortOrder,
                    is_active AS IsActive,
                    created_at AS CreatedAt,
                    updated_at AS UpdatedAt
                FROM categories
                WHERE id = @Id";

            return await connection.QueryFirstOrDefaultAsync<CategoryDto>(sql, new { Id = id });
        }

        #endregion

        #region MUTATION Operations (Entity Framework Core)

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                TenantId = dto.TenantId != Guid.Empty ? dto.TenantId : Guid.NewGuid(),
                ParentId = dto.ParentId,
                Name = dto.Name,
                SortOrder = dto.SortOrder,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _efContext.Categories.Add(category);
            await _efContext.SaveChangesAsync();

            return new CategoryDto
            {
                Id = category.Id,
                TenantId = category.TenantId,
                ParentId = category.ParentId,
                Name = category.Name,
                SortOrder = category.SortOrder,
                IsActive = category.IsActive,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            };
        }

        public async Task<CategoryDto?> UpdateCategoryAsync(Guid id, UpdateCategoryDto dto)
        {
            var category = await _efContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return null;
            }

            category.Name = dto.Name;
            category.ParentId = dto.ParentId;
            category.SortOrder = dto.SortOrder;
            category.IsActive = dto.IsActive;
            category.UpdatedAt = DateTime.UtcNow;

            await _efContext.SaveChangesAsync();

            return new CategoryDto
            {
                Id = category.Id,
                TenantId = category.TenantId,
                ParentId = category.ParentId,
                Name = category.Name,
                SortOrder = category.SortOrder,
                IsActive = category.IsActive,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            };
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var category = await _efContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return false;
            }

            _efContext.Categories.Remove(category);
            await _efContext.SaveChangesAsync();
            return true;
        }

        #endregion
    }
}
