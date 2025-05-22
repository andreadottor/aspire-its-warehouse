namespace ITS.WarehouseApp.ApiService.Endpoints;

using ITS.WarehouseApp.ApiService.Services;
using ITS.WarehouseApp.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

public static class WarehouseStockEndpoints
{
    public static IEndpointRouteBuilder MapWarehouseStockEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/warehouse")
                             .WithTags("warehouse");

        group.MapGet("stockitems", GetWarehouseStockItemsAsync)
            .WithName("GetWarehouseStockItems")
            .WithSummary("Returns all warehouse stock items.")
            .WithDescription("Retrieves the list of all items present in the warehouse.")
            .WithOpenApi(operation =>
            {
                operation.Responses["200"].Description = "List of warehouse stock items";
                return operation;
            });

        group.MapPost("stockmovement", CreateStockMovementAsync)
            .WithName("CreateStockMovement")
            .WithSummary("Creates a warehouse stock movement.")
            .WithDescription("Registers a new stock movement for a specific warehouse item.")
            .WithOpenApi(operation =>
            {
                operation.RequestBody = new Microsoft.OpenApi.Models.OpenApiRequestBody
                {
                    Description = "Data for creating the warehouse stock movement",
                    Required = true
                };
                operation.Responses["200"].Description = "Movement created successfully";
                operation.Responses["400"].Description = "Invalid request";
                return operation;
            });

        group.MapPost("stockitems", CreateWarehouseStockItemAsync)
            .WithName("CreateWarehouseStockItem")
            .WithSummary("Creates a new warehouse stock item.")
            .WithDescription("Registers a new item in the warehouse stock.")
            .WithOpenApi(operation =>
            {
                operation.RequestBody = new Microsoft.OpenApi.Models.OpenApiRequestBody
                {
                    Description = "Data for creating the warehouse stock item",
                    Required = true
                };
                operation.Responses["200"].Description = "Item created successfully";
                operation.Responses["400"].Description = "Invalid request";
                return operation;
            });

        return endpoints;
    }

    private static async Task<Ok<IEnumerable<WarehouseStockItemDto>>> GetWarehouseStockItemsAsync(IWarehouseStockService service)
    {
        var items = await service.GetAllStockItemsAsync();
        return TypedResults.Ok(items.Select(item => new WarehouseStockItemDto
        {
            Id = item.Id,
            ProductName = item.ProductName,
            QuantityInStock = item.QuantityInStock
        }));
    }

    private static async Task<Results<Ok, BadRequest>> CreateStockMovementAsync(IWarehouseStockService service, CreateStockMovementDto dto)
    {
        var movement = new Models.StockMovement
        {
            ItemId = dto.ItemId,
            Quantity = dto.Quantity,
            DateCreation = DateTime.UtcNow
        };
        var result = await service.AddStockMovementAsync(movement);
        return result ? TypedResults.Ok() : TypedResults.BadRequest();
    }

    private static async Task<Results<Ok, BadRequest>> CreateWarehouseStockItemAsync(IWarehouseStockService service, CreateWarehouseStockItemDto dto)
    {
        var item = new Models.WarehouseStockItem
        {
            ProductName = dto.ProductName,
            QuantityInStock = dto.QuantityInStock,
            DateCreation = DateTime.UtcNow,
            DateLastUpdate = DateTime.UtcNow
        };
        var result = await service.AddStockItemAsync(item);
        return result ? TypedResults.Ok() : TypedResults.BadRequest();
    }
}