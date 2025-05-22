using System.Data;
using Dapper;
using ITS.WarehouseApp.ApiService.Models;
using ITS.WarehouseApp.ApiService.Services;

namespace ITS.WarehouseApp.ApiService.Repositories;

public class StockMovementRepository : IStockMovementRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public StockMovementRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> CreateAsync(StockMovement movement)
    {
        var sql = """
            INSERT INTO StockMovements (ItemId, Quantity, DateCreation)
            VALUES (@ItemId, @Quantity, GETDATE());
            SELECT CAST(SCOPE_IDENTITY() as int);
            """;
        return await _unitOfWork.Connection.QuerySingleAsync<int>(sql, movement, _unitOfWork.Transaction);
    }

    public async Task<StockMovement?> GetByIdAsync(int id)
    {
        var sql = """
            SELECT 
                Id,
                ItemId, 
                Quantity, 
                DateCreation
            FROM StockMovements WHERE Id = @Id
            """;

        return await _unitOfWork.Connection.QuerySingleOrDefaultAsync<StockMovement>(sql, new { Id = id }, _unitOfWork.Transaction);
    }

    public async Task<IEnumerable<StockMovement>> GetAllAsync()
    {
        var sql = """
            SELECT 
                Id,
                ItemId, 
                Quantity, 
                DateCreation
            FROM StockMovements
            """;

        return await _unitOfWork.Connection.QueryAsync<StockMovement>(sql, transaction: _unitOfWork.Transaction);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var sql = "DELETE FROM StockMovement WHERE Id = @Id";
        var rows = await _unitOfWork.Connection.ExecuteAsync(sql, new { Id = id }, _unitOfWork.Transaction);
        return rows > 0;
    }
}
