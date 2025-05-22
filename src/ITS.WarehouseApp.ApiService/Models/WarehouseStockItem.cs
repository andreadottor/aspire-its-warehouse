namespace ITS.WarehouseApp.ApiService.Models;

public class WarehouseStockItem
{
    public int Id { get; set; }
    public string ProductName { get; set; } = default!;
    public int QuantityInStock { get; set; }
    public DateTime DateCreation { get; set; }
    public DateTime DateLastUpdate { get; set; }
}
