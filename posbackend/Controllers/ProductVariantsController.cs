using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using posbackend.DTOs;
using posbackend.Services;

namespace posbackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/product-variants")]
    public class ProductVariantsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductVariantsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// 6. แก้ไข Variant (Body: sku, barcode, cost_price, sell_price, attributes)
        /// Uses EF Core
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateVariant(Guid id, [FromBody] UpdateProductVariantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedVariant = await _productService.UpdateVariantAsync(id, dto);
            if (updatedVariant == null)
            {
                return NotFound(new { message = $"Product variant with ID {id} not found." });
            }

            return Ok(updatedVariant);
        }
    }
}
