namespace ITS.WarehouseApp.ApiService.Models;

public class StockMovement
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; }

    public DateTime DateCreation { get; set; }
}
