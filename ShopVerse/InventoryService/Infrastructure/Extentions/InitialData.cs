namespace Inventory.Infrastructure.Extensions;

internal static class InitialData
{
    public static List<InventoryItem> GetInventoryItems()
    {
        return new List<InventoryItem>
        {
            new InventoryItem(
                id: InventoryItemId.Of(Guid.NewGuid()),
                name: "Laptop",
                quantity: 10,
                productId: ProductId.Of(Guid.NewGuid())
            ),
            new InventoryItem(
                id: InventoryItemId.Of(Guid.NewGuid()),
                name: "Mouse",
                quantity: 50,
                productId: ProductId.Of(Guid.NewGuid())
            ),
            new InventoryItem(
                id: InventoryItemId.Of(Guid.NewGuid()),
                name: "Keyboard",
                quantity: 30,
                productId: ProductId.Of(Guid.NewGuid())
            ),
            new InventoryItem(
                id: InventoryItemId.Of(Guid.NewGuid()),
                name: "Monitor",
                quantity: 20,
                productId: ProductId.Of(Guid.NewGuid())
            )
        };
    }
}
