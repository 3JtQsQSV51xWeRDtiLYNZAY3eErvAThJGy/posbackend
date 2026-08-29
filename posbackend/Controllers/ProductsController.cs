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
    [Route("api/v1/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// 1. ดึงรายการสินค้า (Params: search, category_id, item_type, page, limit)
        /// Uses Dapper
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProducts([FromQuery] ProductQueryParameters queryParams)
        {
            var result = await _productService.GetProductsAsync(queryParams);
            return Ok(result);
        }

        /// <summary>
        /// 2. ดึงรายละเอียดสินค้า (Params: :id)
        /// Uses Dapper
        /// </summary>
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(new { message = $"Product with ID {id} not found." });
            }

            return Ok(product);
        }

        /// <summary>
        /// 3. สร้างสินค้าใหม่ (Body: name, category_id, item_type, track_stock, variants)
        /// Uses EF Core
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest(new { message = "Product name is required." });
            }

            var createdProduct = await _productService.CreateProductAsync(dto);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }

        /// <summary>
        /// 4. แก้ไขสินค้า (Body: name, category_id, item_type, track_stock, is_active)
        /// Uses EF Core
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedProduct = await _productService.UpdateProductAsync(id, dto);
            if (updatedProduct == null)
            {
                return NotFound(new { message = $"Product with ID {id} not found." });
            }

            return Ok(updatedProduct);
        }

        /// <summary>
        /// 5. ลบสินค้า (Params: :id)
        /// Uses EF Core
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var success = await _productService.DeleteProductAsync(id);
            if (!success)
            {
                return NotFound(new { message = $"Product with ID {id} not found or already deleted." });
            }

            return NoContent();
        }
    }
}
