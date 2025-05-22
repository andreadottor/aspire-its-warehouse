namespace ITS.WarehouseApp.Web.Components.Pages.Warehouse;

using ITS.WarehouseApp.Dtos;
using Microsoft.AspNetCore.Components;

public partial class Index
{
    private IEnumerable<WarehouseStockItemDto>? _items;

    protected override async Task OnInitializedAsync()
    {
        _items = await ApiClient.GetWarehouseStockItemsAsync();
    }
}
