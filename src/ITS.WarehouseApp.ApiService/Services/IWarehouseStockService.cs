namespace ITS.WarehouseApp.ApiService.Services
{
    using ITS.WarehouseApp.ApiService.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IWarehouseStockService
    {
        Task<bool> AddStockMovementAsync(StockMovement movement);
        Task<IEnumerable<WarehouseStockItem>> GetAllStockItemsAsync();
        Task<bool> AddStockItemAsync(WarehouseStockItem item);
    }
}