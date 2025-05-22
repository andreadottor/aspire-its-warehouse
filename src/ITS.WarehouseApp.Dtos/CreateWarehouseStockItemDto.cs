namespace ITS.WarehouseApp.Dtos;

public class CreateWarehouseStockItemDto
{
    public string ProductName { get; set; } = string.Empty;
    public int QuantityInStock { get; set; }
}
