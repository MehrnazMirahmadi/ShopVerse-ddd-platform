namespace Application.Inventory.Queries.GetInventoryItems;
public record GetInventoryItemsQuery(
    PaginationRequest Paging,
    string? FilterName = null,
    bool SortByQuantityDesc = false
) : IQuery<Result<PaginationResult<InventoryItemDto>>>;

public record InventoryItemsResult(IReadOnlyList<InventoryItemDto> InventoryItems);
