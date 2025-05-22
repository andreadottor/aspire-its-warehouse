var builder = DistributedApplication.CreateBuilder(args);

var azureSql = builder.AddConnectionString("WarehouseDb");

var apiService = builder.AddProject<Projects.ITS_WarehouseApp_ApiService>("apiservice")
    .WithReference(azureSql)
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.ITS_WarehouseApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();

