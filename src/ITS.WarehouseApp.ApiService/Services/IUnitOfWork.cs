using System.Data;

namespace ITS.WarehouseApp.ApiService.Services;

public interface IUnitOfWork : IDisposable
{
    IDbConnection Connection { get; }
    IDbTransaction Transaction { get; }
    void Begin();
    void Commit();
    void Rollback();
}
