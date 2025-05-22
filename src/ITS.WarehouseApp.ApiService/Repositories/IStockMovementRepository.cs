namespace ITS.WarehouseApp.ApiService.Repositories
{
    using ITS.WarehouseApp.ApiService.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Data;

    public interface IStockMovementRepository
    {
        Task<int> CreateAsync(StockMovement movement);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<StockMovement>> GetAllAsync();
        Task<StockMovement?> GetByIdAsync(int id);
    }
}