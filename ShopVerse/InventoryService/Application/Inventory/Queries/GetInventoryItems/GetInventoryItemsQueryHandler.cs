namespace Application.Inventory.Queries.GetInventoryItems;

public class GetInventoryItemsQueryHandler : IQueryHandler<GetInventoryItemsQuery, Result<InventoryItemsResult>>
{
    private readonly IUnitOfWork unitOfWork;

    public GetInventoryItemsQueryHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<InventoryItemsResult>> Handle(GetInventoryItemsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var inventoryItems = await unitOfWork.InventoryRepository.GetAllAsync(cancellationToken);

            var inventoryDtos = inventoryItems.Select(i => new InventoryItemDto(
                i.Id.Value,
                i.Name,
                i.Quantity
            )).ToList();

            var resultData = new InventoryItemsResult(inventoryDtos);

            return Result<InventoryItemsResult>.Success(resultData);
        }
        catch (Exception ex)
        {
            return Result<InventoryItemsResult>.Fail($"Failed to get inventory items: {ex.Message}");
        }
    }
}
