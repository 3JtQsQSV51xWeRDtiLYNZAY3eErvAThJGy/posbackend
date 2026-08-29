using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using posbackend.DTOs;

namespace posbackend.Services
{
    public interface IStockLocationService
    {
        Task<IEnumerable<StockLocationDto>> GetStockLocationsAsync(StockLocationQueryParameters queryParams);
        Task<StockLocationDto?> GetStockLocationByIdAsync(Guid id);
        Task<StockLocationDto> CreateStockLocationAsync(CreateStockLocationDto dto);
        Task<StockLocationDto?> UpdateStockLocationAsync(Guid id, UpdateStockLocationDto dto);
        Task<bool> DeleteStockLocationAsync(Guid id);
    }
}
