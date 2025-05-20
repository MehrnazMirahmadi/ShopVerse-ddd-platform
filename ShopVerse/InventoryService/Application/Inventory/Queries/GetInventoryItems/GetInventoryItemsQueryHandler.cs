namespace Application.Inventory.Queries.GetInventoryItems;

public class GetInventoryItemsQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetInventoryItemsQuery, InventoryItemsResult>
{
    public async Task<InventoryItemsResult> Handle(GetInventoryItemsQuery request, CancellationToken cancellationToken)
    {
        var inventoryItems = await unitOfWork.InventoryRepository.GetAllAsync(cancellationToken);

        var inventoryDtos = inventoryItems.Select(i => new InventoryItemDto(
            i.Id.Value,
            i.Name,
            i.Quantity
        )).ToList();

        return new InventoryItemsResult(inventoryDtos);
    }
}
