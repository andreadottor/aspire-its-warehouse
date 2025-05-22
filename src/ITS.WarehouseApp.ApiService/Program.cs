using ITS.WarehouseApp.ApiService.Repositories;
using ITS.WarehouseApp.ApiService.Services;
using ITS.WarehouseApp.ApiService.Endpoints;
using System.Data;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.AddSqlServerClient(connectionName: "WarehouseDb");
builder.Services.AddScoped<IDbConnection>(services => services.GetRequiredService<SqlConnection>());

builder.Services.AddScoped<IWarehouseStockService, WarehouseStockService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IStockMovementRepository, StockMovementRepository>();
builder.Services.AddScoped<IWarehouseStockItemRepository, WarehouseStockItemRepository>();

var app = builder.Build();

await SeedData(app);

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Warehouse API V1"));

    app.MapGet("/", () => Results.Redirect("/swagger"));
}

app.MapWarehouseStockEndpoints();
app.MapDefaultEndpoints();

app.Run();

static async Task SeedData(WebApplication app)
{
    // SEED DATI INIZIALI SE LA TABELLA WarehouseStockItem È VUOTA
    using (var scope = app.Services.CreateScope())
    {
        var repo = scope.ServiceProvider.GetRequiredService<IWarehouseStockItemRepository>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var items = await repo.GetAllAsync();
        if (!items.Any())
        {
            var random = new Random();
            var now = DateTime.UtcNow;
            unitOfWork.Begin();
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    var item = new ITS.WarehouseApp.ApiService.Models.WarehouseStockItem
                    {
                        ProductName = $"Prodotto {i}",
                        QuantityInStock = random.Next(1, 100),
                        DateCreation = now,
                        DateLastUpdate = now
                    };
                    await repo.CreateAsync(item);
                }
                unitOfWork.Commit();
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }
    }
}
