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
    [Route("api/v1/stock-locations")]
    public class StockLocationsController : ControllerBase
    {
        private readonly IStockLocationService _stockLocationService;

        public StockLocationsController(IStockLocationService stockLocationService)
        {
            _stockLocationService = stockLocationService;
        }

        /// <summary>
        /// 1. ดึงคลังสินค้า (Params: store_id)
        /// Uses Dapper
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetStockLocations([FromQuery] StockLocationQueryParameters queryParams)
        {
            var locations = await _stockLocationService.GetStockLocationsAsync(queryParams);
            return Ok(locations);
        }

        /// <summary>
        /// ดึงรายละเอียดคลังสินค้าตาม ID
        /// Uses Dapper
        /// </summary>
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetStockLocationById(Guid id)
        {
            var location = await _stockLocationService.GetStockLocationByIdAsync(id);
            if (location == null)
            {
                return NotFound(new { message = $"Stock location with ID {id} not found." });
            }

            return Ok(location);
        }

        /// <summary>
        /// 2. สร้างคลังสินค้า (Body: store_id, name, is_default)
        /// Uses EF Core
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateStockLocation([FromBody] CreateStockLocationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest(new { message = "Stock location name is required." });
            }

            var createdLocation = await _stockLocationService.CreateStockLocationAsync(dto);
            return CreatedAtAction(nameof(GetStockLocationById), new { id = createdLocation.Id }, createdLocation);
        }

        /// <summary>
        /// 3. แก้ไขคลัง (Body: name, is_default, is_active)
        /// Uses EF Core
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateStockLocation(Guid id, [FromBody] UpdateStockLocationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedLocation = await _stockLocationService.UpdateStockLocationAsync(id, dto);
            if (updatedLocation == null)
            {
                return NotFound(new { message = $"Stock location with ID {id} not found." });
            }

            return Ok(updatedLocation);
        }

        /// <summary>
        /// 4. ลบคลัง (Params: :id)
        /// Uses EF Core
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteStockLocation(Guid id)
        {
            var success = await _stockLocationService.DeleteStockLocationAsync(id);
            if (!success)
            {
                return NotFound(new { message = $"Stock location with ID {id} not found." });
            }

            return Ok(new { message = $"Stock location with ID {id} deleted successfully." });
        }
    }
}
