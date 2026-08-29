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
    [Route("api/v1/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// 1. ดึงหมวดหมู่ (Params: tenant_id, parent_id)
        /// Uses Dapper
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategories([FromQuery] CategoryQueryParameters queryParams)
        {
            var categories = await _categoryService.GetCategoriesAsync(queryParams);
            return Ok(categories);
        }

        /// <summary>
        /// ดึงรายละเอียดหมวดหมู่ตาม ID
        /// Uses Dapper
        /// </summary>
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound(new { message = $"Category with ID {id} not found." });
            }

            return Ok(category);
        }

        /// <summary>
        /// 2. สร้างหมวดหมู่ (Body: name, parent_id, sort_order)
        /// Uses EF Core
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest(new { message = "Category name is required." });
            }

            var createdCategory = await _categoryService.CreateCategoryAsync(dto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
        }

        /// <summary>
        /// 3. แก้ไขหมวดหมู่ (Body: name, parent_id, sort_order, is_active)
        /// Uses EF Core
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedCategory = await _categoryService.UpdateCategoryAsync(id, dto);
            if (updatedCategory == null)
            {
                return NotFound(new { message = $"Category with ID {id} not found." });
            }

            return Ok(updatedCategory);
        }

        /// <summary>
        /// 4. ลบหมวดหมู่ (Params: :id)
        /// Uses EF Core
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var success = await _categoryService.DeleteCategoryAsync(id);
            if (!success)
            {
                return NotFound(new { message = $"Category with ID {id} not found." });
            }

            return Ok(new { message = $"Category with ID {id} deleted successfully." });
        }
    }
}
