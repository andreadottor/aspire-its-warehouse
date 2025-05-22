using ITS.WarehouseApp.Dtos;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;

namespace ITS.WarehouseApp.Web;

public class WarehouseApiClient(HttpClient httpClient)
{
    public async Task<List<WarehouseStockItemDto>> GetWarehouseStockItemsAsync(CancellationToken cancellationToken = default)
    {
        var items = await httpClient.GetFromJsonAsync<List<WarehouseStockItemDto>>("/api/warehouse/stockitems", cancellationToken);
        return items ?? [];
    }

    public async Task<bool> CreateStockMovementAsync(CreateStockMovementDto dto, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/api/warehouse/stockmovement", dto, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateWarehouseStockItemAsync(CreateWarehouseStockItemDto dto, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/api/warehouse/stockitems", dto, cancellationToken);
        return response.IsSuccessStatusCode;
    }
}
