using ITS.WarehouseApp.ApiService.Models;
using ITS.WarehouseApp.ApiService.Repositories;
using System.Data;

namespace ITS.WarehouseApp.ApiService.Services;

public class WarehouseStockService : IWarehouseStockService
{
    private readonly IWarehouseStockItemRepository _stockItemRepository;
    private readonly IStockMovementRepository _stockMovementRepository;
    private readonly IUnitOfWork _unitOfWork;

    public WarehouseStockService(
        IWarehouseStockItemRepository stockItemRepository,
        IStockMovementRepository stockMovementRepository,
        IUnitOfWork unitOfWork)
    {
        _stockItemRepository = stockItemRepository;
        _stockMovementRepository = stockMovementRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<WarehouseStockItem>> GetAllStockItemsAsync()
    {
        return await _stockItemRepository.GetAllAsync();
    }

    public async Task<bool> AddStockMovementAsync(StockMovement movement)
    {
        try
        {
            _unitOfWork.Begin();

            await _stockMovementRepository.CreateAsync(movement);

            var stockItem = await _stockItemRepository.GetByIdAsync(movement.ItemId);
            if (stockItem == null)
            {
                _unitOfWork.Rollback();
                return false;
            }

            stockItem.QuantityInStock += movement.Quantity;
            stockItem.DateLastUpdate = DateTime.UtcNow;
            var updated = await _stockItemRepository.UpdateAsync(stockItem);

            if (!updated)
            {
                _unitOfWork.Rollback();
                return false;
            }

            _unitOfWork.Commit();
            return true;
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }
    }

    public async Task<bool> AddStockItemAsync(WarehouseStockItem item)
    {
        try
        {
            _unitOfWork.Begin();
            await _stockItemRepository.CreateAsync(item);
            _unitOfWork.Commit();
            return true;
        }
        catch
        {
            _unitOfWork.Rollback();
            return false;
        }
    }
}
