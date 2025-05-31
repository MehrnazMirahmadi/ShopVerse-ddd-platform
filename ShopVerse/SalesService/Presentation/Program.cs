using Application;
using Application.GrpcInterface;
using Infrastructure;
using Infrastructure.Persistence.Context;
using InventoryGrpc;
using Microsoft.EntityFrameworkCore;
using Presentation.GrpcServices;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Register gRPC service client (if this is a gRPC client consumer)
builder.Services.AddGrpcClient<InventoryService.InventoryServiceClient>(o =>
{
    o.Address = new Uri("https://localhost:5051");
});
builder.Services.AddScoped<IInventoryServiceClient, GrpcInventoryServiceClient>();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    dbContext.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
