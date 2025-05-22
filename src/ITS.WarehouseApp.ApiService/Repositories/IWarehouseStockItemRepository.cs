namespace ITS.WarehouseApp.ApiService.Repositories
{
    using ITS.WarehouseApp.ApiService.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Data;

    public interface IWarehouseStockItemRepository
    {
        Task<int> CreateAsync(WarehouseStockItem item);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<WarehouseStockItem>> GetAllAsync();
        Task<WarehouseStockItem?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(WarehouseStockItem item);
    }
}