using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using posbackend.Data;
using posbackend.DTOs;
using posbackend.Models;

namespace posbackend.Services
{
    public class ProductService : IProductService
    {
        private readonly DapperContext _dapperContext;
        private readonly AppDbContext _efContext;

        public ProductService(DapperContext dapperContext, AppDbContext efContext)
        {
            _dapperContext = dapperContext;
            _efContext = efContext;
        }

        #region SELECT Operations (Dapper)

        public async Task<PagedResultDto<ProductDto>> GetProductsAsync(ProductQueryParameters queryParams)
        {
            using var connection = _dapperContext.CreateConnection();

            var whereClauses = new List<string> { "p.deleted_at IS NULL" };
            var parameters = new DynamicParameters();

            if (!string.IsNullOrWhiteSpace(queryParams.Search))
            {
                whereClauses.Add("(p.name LIKE @Search OR p.description LIKE @Search)");
                parameters.Add("Search", $"%{queryParams.Search}%");
            }

            if (queryParams.CategoryId.HasValue && queryParams.CategoryId.Value != Guid.Empty)
            {
                whereClauses.Add("p.category_id = @CategoryId");
                parameters.Add("CategoryId", queryParams.CategoryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(queryParams.ItemType))
            {
                whereClauses.Add("p.item_type = @ItemType");
                parameters.Add("ItemType", queryParams.ItemType);
            }

            string whereSql = string.Join(" AND ", whereClauses);

            // Count Query
            string countSql = $"SELECT COUNT(1) FROM products p WHERE {whereSql}";
            int totalCount = await connection.ExecuteScalarAsync<int>(countSql, parameters);

            // Pagination setup
            int page = queryParams.Page < 1 ? 1 : queryParams.Page;
            int limit = queryParams.Limit < 1 ? 10 : queryParams.Limit;
            int offset = (page - 1) * limit;

            parameters.Add("Limit", limit);
            parameters.Add("Offset", offset);

            // Product List Query
            string querySql = $@"
                SELECT 
                    p.id AS Id,
                    p.tenant_id AS TenantId,
                    p.category_id AS CategoryId,
                    c.name AS CategoryName,
                    p.name AS Name,
                    p.description AS Description,
                    p.item_type AS ItemType,
                    p.track_stock AS TrackStock,
                    p.is_purchaseable AS IsPurchaseable,
                    p.duration_minutes AS DurationMinutes,
                    p.is_active AS IsActive,
                    p.created_at AS CreatedAt,
                    p.updated_at AS UpdatedAt
                FROM products p
                LEFT JOIN categories c ON p.category_id = c.id
                WHERE {whereSql}
                ORDER BY p.created_at DESC
                LIMIT @Limit OFFSET @Offset";

            var products = (await connection.QueryAsync<ProductDto>(querySql, parameters)).ToList();

            // Fetch variants for returned products
            if (products.Any())
            {
                var productIds = products.Select(p => p.Id).ToList();
                string variantSql = @"
                    SELECT 
                        id AS Id,
                        product_id AS ProductId,
                        sku AS Sku,
                        barcode AS Barcode,
                        cost_price AS CostPrice,
                        sell_price AS SellPrice,
                        attributes AS Attributes,
                        is_active AS IsActive,
                        created_at AS CreatedAt
                    FROM product_variants
                    WHERE product_id IN @ProductIds";

                var variants = (await connection.QueryAsync<ProductVariantDto>(variantSql, new { ProductIds = productIds })).ToList();
                var variantLookup = variants.GroupBy(v => v.ProductId).ToDictionary(g => g.Key, g => g.ToList());
                foreach (var product in products)
                {
                    if (variantLookup.TryGetValue(product.Id, out var productVariants))
                    {
                        product.Variants = productVariants;
                    }
                }
            }

            return new PagedResultDto<ProductDto>
            {
                TotalCount = totalCount,
                Page = page,
                Limit = limit,
                Data = products
            };
        }

        public async Task<ProductDto?> GetProductByIdAsync(Guid id)
        {
            using var connection = _dapperContext.CreateConnection();

            string sql = @"
                SELECT 
                    p.id AS Id,
                    p.tenant_id AS TenantId,
                    p.category_id AS CategoryId,
                    c.name AS CategoryName,
                    p.name AS Name,
                    p.description AS Description,
                    p.item_type AS ItemType,
                    p.track_stock AS TrackStock,
                    p.is_purchaseable AS IsPurchaseable,
                    p.duration_minutes AS DurationMinutes,
                    p.is_active AS IsActive,
                    p.created_at AS CreatedAt,
                    p.updated_at AS UpdatedAt
                FROM products p
                LEFT JOIN categories c ON p.category_id = c.id
                WHERE p.id = @Id AND p.deleted_at IS NULL;

                SELECT 
                    id AS Id,
                    product_id AS ProductId,
                    sku AS Sku,
                    barcode AS Barcode,
                    cost_price AS CostPrice,
                    sell_price AS SellPrice,
                    attributes AS Attributes,
                    is_active AS IsActive,
                    created_at AS CreatedAt
                FROM product_variants
                WHERE product_id = @Id;";

            using var multi = await connection.QueryMultipleAsync(sql, new { Id = id });
            var product = await multi.ReadFirstOrDefaultAsync<ProductDto>();
            if (product != null)
            {
                product.Variants = (await multi.ReadAsync<ProductVariantDto>()).ToList();
            }

            return product;
        }

        #endregion

        #region MUTATION Operations (Entity Framework Core)

        public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
        {
            var now = DateTime.UtcNow;

            var product = new Product
            {
                Id = Guid.NewGuid(),
                TenantId = dto.TenantId != Guid.Empty ? dto.TenantId : Guid.NewGuid(),
                CategoryId = dto.CategoryId,
                Name = dto.Name,
                Description = dto.Description ?? string.Empty,
                ItemType = dto.ItemType ?? "PHYSICAL",
                TrackStock = dto.TrackStock,
                IsPurchaseable = dto.IsPurchaseable,
                DurationMinutes = dto.DurationMinutes,
                IsActive = true,
                CreatedAt = now,
                UpdatedAt = now
            };

            _efContext.Products.Add(product);
            await _efContext.SaveChangesAsync();

            if (dto.Variants != null && dto.Variants.Any())
            {
                var variants = dto.Variants.Select(v => new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    Sku = v.Sku ?? string.Empty,
                    Barcode = v.Barcode ?? string.Empty,
                    CostPrice = v.CostPrice,
                    SellPrice = v.SellPrice,
                    Attributes = v.Attributes ?? "{}",
                    IsActive = true,
                    CreatedAt = now
                }).ToList();

                _efContext.ProductVariants.AddRange(variants);
                await _efContext.SaveChangesAsync();
            }

            var createdProduct = await GetProductByIdAsync(product.Id);
            return createdProduct ?? new ProductDto
            {
                Id = product.Id,
                TenantId = product.TenantId,
                CategoryId = product.CategoryId,
                Name = product.Name,
                Description = product.Description,
                ItemType = product.ItemType,
                TrackStock = product.TrackStock,
                IsPurchaseable = product.IsPurchaseable,
                DurationMinutes = product.DurationMinutes,
                IsActive = product.IsActive,
                CreatedAt = product.CreatedAt
            };
        }

        public async Task<ProductDto?> UpdateProductAsync(Guid id, UpdateProductDto dto)
        {
            var product = await _efContext.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.DeletedAt == null);

            if (product == null)
            {
                return null;
            }

            product.Name = dto.Name;
            product.CategoryId = dto.CategoryId;
            product.ItemType = dto.ItemType ?? product.ItemType;
            product.TrackStock = dto.TrackStock;
            product.IsActive = dto.IsActive;
            if (dto.Description != null) product.Description = dto.Description;
            if (dto.IsPurchaseable.HasValue) product.IsPurchaseable = dto.IsPurchaseable.Value;
            if (dto.DurationMinutes.HasValue) product.DurationMinutes = dto.DurationMinutes.Value;
            product.UpdatedAt = DateTime.UtcNow;

            await _efContext.SaveChangesAsync();

            return await GetProductByIdAsync(product.Id);
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = await _efContext.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.DeletedAt == null);

            if (product == null)
            {
                return false;
            }

            product.DeletedAt = DateTime.UtcNow;
            product.IsActive = false;

            await _efContext.SaveChangesAsync();
            return true;
        }

        public async Task<ProductVariantDto?> UpdateVariantAsync(Guid variantId, UpdateProductVariantDto dto)
        {
            var variant = await _efContext.ProductVariants
                .FirstOrDefaultAsync(v => v.Id == variantId);

            if (variant == null)
            {
                return null;
            }

            if (dto.Sku != null) variant.Sku = dto.Sku;
            if (dto.Barcode != null) variant.Barcode = dto.Barcode;
            variant.CostPrice = dto.CostPrice;
            variant.SellPrice = dto.SellPrice;
            if (dto.Attributes != null) variant.Attributes = dto.Attributes;
            if (dto.IsActive.HasValue) variant.IsActive = dto.IsActive.Value;

            await _efContext.SaveChangesAsync();

            return new ProductVariantDto
            {
                Id = variant.Id,
                ProductId = variant.ProductId,
                Sku = variant.Sku,
                Barcode = variant.Barcode,
                CostPrice = variant.CostPrice,
                SellPrice = variant.SellPrice,
                Attributes = variant.Attributes,
                IsActive = variant.IsActive,
                CreatedAt = variant.CreatedAt
            };
        }

        #endregion
    }
}
