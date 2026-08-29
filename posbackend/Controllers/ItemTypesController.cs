using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using posbackend.DTOs;
using posbackend.Services;

namespace posbackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/item-types")]
    public class ItemTypesController : ControllerBase
    {
        private readonly IItemTypeService _itemTypeService;

        public ItemTypesController(IItemTypeService itemTypeService)
        {
            _itemTypeService = itemTypeService;
        }

        /// <summary>
        /// ดึงรายการประเภทสินค้า Item Types (Params: tenant_id, search, is_active, is_service, page, limit)
        /// Uses Dapper
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetItemTypes([FromQuery] ItemTypeQueryParameters queryParams)
        {
            var result = await _itemTypeService.GetItemTypesAsync(queryParams);
            return Ok(result);
        }

        /// <summary>
        /// ดึงรายละเอียดประเภทสินค้า Item Type ตาม ID (Params: :id)
        /// Uses Dapper
        /// </summary>
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetItemTypeById(int id)
        {
            var itemType = await _itemTypeService.GetItemTypeByIdAsync(id);
            if (itemType == null)
            {
                return NotFound(new { message = $"ItemType with ID {id} not found." });
            }

            return Ok(itemType);
        }
    }
}
