using System.Data;
using Dapper;
using ITS.WarehouseApp.ApiService.Models;
using ITS.WarehouseApp.ApiService.Services;

namespace ITS.WarehouseApp.ApiService.Repositories;

public class WarehouseStockItemRepository : IWarehouseStockItemRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public WarehouseStockItemRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> CreateAsync(WarehouseStockItem item)
    {
        var sql = """
            INSERT INTO WarehouseStockItems (ProductName, QuantityInStock, DateCreation, DateLastUpdate)
            VALUES (@ProductName, @QuantityInStock, GETDATE(), GETDATE());
            SELECT CAST(SCOPE_IDENTITY() as int);
            """;
        return await _unitOfWork.Connection.QuerySingleAsync<int>(sql, item, _unitOfWork.Transaction);
    }

    public async Task<WarehouseStockItem?> GetByIdAsync(int id)
    {
        var sql = """
            SELECT 
                Id,
                ProductName, 
                QuantityInStock, 
                DateCreation, 
                DateLastUpdate
            FROM WarehouseStockItems WHERE Id = @Id
            """;

        return await _unitOfWork.Connection.QuerySingleOrDefaultAsync<WarehouseStockItem>(sql, new { Id = id }, _unitOfWork.Transaction);
    }

    public async Task<IEnumerable<WarehouseStockItem>> GetAllAsync()
    {
        var sql = """
            SELECT 
                Id,
                ProductName, 
                QuantityInStock, 
                DateCreation, 
                DateLastUpdate
            FROM WarehouseStockItems
            """;

        return await _unitOfWork.Connection.QueryAsync<WarehouseStockItem>(sql, transaction: _unitOfWork.Transaction);
    }

    public async Task<bool> UpdateAsync(WarehouseStockItem item)
    {
        var sql = """
            UPDATE WarehouseStockItems
            SET 
                ProductName = @ProductName, 
                QuantityInStock = @QuantityInStock, 
                DateLastUpdate = GETDATE() 
            WHERE Id = @Id
            """;

        var rows = await _unitOfWork.Connection.ExecuteAsync(sql, item, _unitOfWork.Transaction);
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var sql = "DELETE FROM WarehouseStockItems WHERE Id = @Id";
        var rows = await _unitOfWork.Connection.ExecuteAsync(sql, new { Id = id }, _unitOfWork.Transaction);
        return rows > 0;
    }
}
