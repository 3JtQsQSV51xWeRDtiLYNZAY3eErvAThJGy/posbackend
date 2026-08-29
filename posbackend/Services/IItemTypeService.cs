using System.Threading.Tasks;
using posbackend.DTOs;

namespace posbackend.Services
{
    public interface IItemTypeService
    {
        /// <summary>
        /// Get list of item types with pagination and filters (Using Dapper)
        /// </summary>
        Task<PagedResultDto<ItemTypeDto>> GetItemTypesAsync(ItemTypeQueryParameters queryParams);

        /// <summary>
        /// Get item type details by ID (Using Dapper)
        /// </summary>
        Task<ItemTypeDto?> GetItemTypeByIdAsync(int id);
    }
}
