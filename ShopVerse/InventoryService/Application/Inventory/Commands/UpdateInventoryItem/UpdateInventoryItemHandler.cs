using Application.Helper;

namespace Application.Inventory.Commands.UpdateInventoryItem;

public class UpdateInventoryItemHandler : ICommandHandler<UpdateInventoryItemCommand, Result<UpdateInventoryItemResult>>
{
    private readonly IUnitOfWork unitOfWork;

    public UpdateInventoryItemHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<UpdateInventoryItemResult>> Handle(UpdateInventoryItemCommand request, CancellationToken cancellationToken)
    {
        var item = await unitOfWork.InventoryRepository.GetByIdAsync(
            InventoryItemId.Of(request.inventoryItem.Id), cancellationToken);

        if (item is null)
            return Result<UpdateInventoryItemResult>.Fail("Inventory item not found.");

        InventoryItemUpdater.ApplyUpdatesFromDto(item, request.inventoryItem);

        await unitOfWork.InventoryRepository.UpdateAsync(item, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        return Result<UpdateInventoryItemResult>.Success(new UpdateInventoryItemResult(true));
    }
}
