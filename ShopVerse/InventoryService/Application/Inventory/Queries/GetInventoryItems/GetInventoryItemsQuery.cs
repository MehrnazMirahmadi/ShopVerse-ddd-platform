using Application.Dtos;
using ShopVerse.BuildingBlocks.CQRS;

namespace Application.Inventory.Queries.GetInventoryItems;

public record GetInventoryItemsQuery() 
    : IQuery<InventoryItemsResult>;
public record InventoryItemsResult(IReadOnlyList<InventoryItemDto> InventoryItems);

