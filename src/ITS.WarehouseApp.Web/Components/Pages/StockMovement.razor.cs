namespace ITS.WarehouseApp.Web.Components.Pages;

using ITS.WarehouseApp.Dtos;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

public partial class StockMovement
{
    private IEnumerable<WarehouseStockItemDto>? _items;
    private CreateStockMovementVM _movement = new();
    private string? _message;
    private string _alertClass = "alert-success";

    protected override async Task OnInitializedAsync()
    {
        _items = await ApiClient.GetWarehouseStockItemsAsync();
    }

    private async Task HandleValidSubmit()
    {
        var createMovement = new CreateStockMovementDto
        {
            ItemId = _movement.ItemId!.Value,
            Quantity = _movement.Quantity
        };

        var success = await ApiClient.CreateStockMovementAsync(createMovement);
        if (success)
        {
            _message = "Movimentazione eseguita con successo.";
            _alertClass = "alert-success";
        }
        else
        {
            _message = "Errore durante la movimentazione.";
            _alertClass = "alert-danger";
        }
    }

    class CreateStockMovementVM
    {
        [Required]
        public int? ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
