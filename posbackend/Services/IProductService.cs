using System;
using System.Threading.Tasks;
using posbackend.DTOs;

namespace posbackend.Services
{
    public interface IProductService
    {
        Task<PagedResultDto<ProductDto>> GetProductsAsync(ProductQueryParameters queryParams);
        Task<ProductDto?> GetProductByIdAsync(Guid id);
        Task<ProductDto> CreateProductAsync(CreateProductDto dto);
        Task<ProductDto?> UpdateProductAsync(Guid id, UpdateProductDto dto);
        Task<bool> DeleteProductAsync(Guid id);
        Task<ProductVariantDto?> UpdateVariantAsync(Guid variantId, UpdateProductVariantDto dto);
    }
}
