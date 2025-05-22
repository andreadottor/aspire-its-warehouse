namespace ITS.WarehouseApp.Dtos;

public class WarehouseStockItemDto
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int QuantityInStock { get; set; }
}