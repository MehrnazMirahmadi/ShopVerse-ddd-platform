namespace Application.Inventory.Queries.GetInventoryItems;

public record GetInventoryItemsQuery()
    : IQuery<Result<InventoryItemsResult>>;
public record InventoryItemsResult(IReadOnlyList<InventoryItemDto> InventoryItems);

