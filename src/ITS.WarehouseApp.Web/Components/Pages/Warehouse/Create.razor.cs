namespace ITS.WarehouseApp.Web.Components.Pages.Warehouse;

using ITS.WarehouseApp.Dtos;
using System.ComponentModel.DataAnnotations;

public partial class Create
{
    private CreateWarehouseStockItemVM _item = new();
    private string? _message;
    private string _alertClass = "alert-success";

    private async Task HandleValidSubmit()
    {
        var dto = new CreateWarehouseStockItemDto
        {
            ProductName = _item.ProductName,
            QuantityInStock = _item.QuantityInStock
        };
        var success = await ApiClient.CreateWarehouseStockItemAsync(dto);
        if (success)
        {
            Navigation.NavigateTo("/warehouse");
        }
        else
        {
            _message = "Errore durante la creazione.";
            _alertClass = "alert-danger";
        }
    }

    public class CreateWarehouseStockItemVM
    {
        [Required]
        public string ProductName { get; set; } = string.Empty;
        [Range(0, int.MaxValue)]
        public int QuantityInStock { get; set; }
    }
}
