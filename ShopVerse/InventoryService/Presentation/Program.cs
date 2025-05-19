using Application;
using Application.Dtos;
using Application.Inventory.Queries.GetInventoryItems;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
app.Use(async (context, next) =>
{
    Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
    await next();
    Console.WriteLine($"Response: {context.Response.StatusCode}");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/inventory", async ([FromServices] ISender sender, CancellationToken cancellationToken) =>
{
    var result = await sender.Send(new GetInventoryItemsQuery(), cancellationToken);
    return Results.Ok(result.InventoryItems);
})
.WithName("GetInventoryItems")
.WithTags("Inventory")
.Produces<List<InventoryItemDto>>(StatusCodes.Status200OK);

app.Run();
