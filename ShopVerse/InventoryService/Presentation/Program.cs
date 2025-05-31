using Presentation;
using Inventory.Presentation.GrpcServices;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration)
.AddApplicationServices().AddApiServices();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc();
builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("Database")!);
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
    await dbContext.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<InventoryServiceImpl>();
app.MapGet("/", () => "This service is for gRPC only.");
app.MapHealthChecks("/health");
app.Run();



