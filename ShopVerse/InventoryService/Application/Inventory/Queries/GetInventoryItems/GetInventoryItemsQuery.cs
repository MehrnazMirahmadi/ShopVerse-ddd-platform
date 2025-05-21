namespace Application.Inventory.Queries.GetInventoryItems;
public record GetInventoryItemsQuery(PaginationRequest Paging) : IQuery<Result<PaginationResult<InventoryItemDto>>>;

public record InventoryItemsResult(IReadOnlyList<InventoryItemDto> InventoryItems);
