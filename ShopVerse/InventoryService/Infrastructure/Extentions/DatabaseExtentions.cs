using Inventory.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extentions;

public static class DatabaseExtentions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();

        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedAsync(context);
    }

    private static async Task SeedAsync(InventoryDbContext context)
    {
        await SeedInventoryItemAsync(context);

    }

    private static async Task SeedInventoryItemAsync(InventoryDbContext context)
    {
        if (!await context.InventoryItems.AnyAsync())
        {
            await context.InventoryItems.AddRangeAsync(InitialData.GetInventoryItems());
            await context.SaveChangesAsync();
        }
    }




}