namespace Application.Helper;

public static class InventoryItemUpdater
{
    public static void ApplyUpdatesFromDto(InventoryItem item, InventoryItemDto dto)
    {
        typeof(InventoryItem)
            .GetProperty(nameof(item.Name))!
            .SetValue(item, dto.Name);

        typeof(InventoryItem)
            .GetProperty(nameof(item.Quantity))!
            .SetValue(item, dto.Quantity);
    }
}
