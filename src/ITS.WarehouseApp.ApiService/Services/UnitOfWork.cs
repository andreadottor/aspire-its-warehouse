using System.Data;

namespace ITS.WarehouseApp.ApiService.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly IDbConnection _connection;
    private IDbTransaction? _transaction;
    private bool _disposed;

    public UnitOfWork(IDbConnection connection)
    {
        _connection = connection;
    }

    public IDbConnection Connection => _connection;
    public IDbTransaction Transaction => _transaction!;

    public void Begin()
    {
        if (_transaction == null)
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            _transaction = _connection.BeginTransaction();
        }
    }

    public void Commit()
    {
        _transaction?.Commit();
        Dispose();
    }

    public void Rollback()
    {
        _transaction?.Rollback();
        Dispose();
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _transaction?.Dispose();
            _transaction = null;
            _disposed = true;
        }
    }
}
