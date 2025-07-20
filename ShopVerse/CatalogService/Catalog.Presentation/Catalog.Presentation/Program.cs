using Catalog.Application;
using Catalog.Application.GrpcInterface;
using Catalog.Infrastructure;
using Catalog.Infrastructure.Persistence.Context;
using Catalog.Presentation.GrpcSevices;
using Inventory.Grpc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration);
builder.Services.AddControllers();
// Register gRPC service client (if this is a gRPC client consumer)
builder.Services
    .AddGrpcClient<ProductInventoryService.ProductInventoryServiceClient>(o =>
{
    o.Address = new Uri("http://productinventory.grpc:8090");

});
builder.Services.AddScoped<IProductInventoryGrpcClient, ProductInventoryGrpcClient>();
var app = builder.Build();

// Configure the HTTP request pipeline.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    dbContext.Database.Migrate();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
