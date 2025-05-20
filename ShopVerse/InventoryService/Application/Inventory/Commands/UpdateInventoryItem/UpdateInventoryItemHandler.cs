using Application.Helper;

namespace Application.Inventory.Commands.UpdateInventoryItem;

public class UpdateInventoryItemHandler(IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateInventoryItemCommand, UpdateInventoryItemResult>
{
    public async Task<UpdateInventoryItemResult> Handle(UpdateInventoryItemCommand request, CancellationToken cancellationToken)
    {
        var item = await unitOfWork.InventoryRepository.GetByIdAsync(
            InventoryItemId.Of(request.inventoryItem.Id), cancellationToken);

        if (item is null)
            throw new InvalidOperationException("Inventory item not found.");

    
        InventoryItemUpdater.ApplyUpdatesFromDto(item, request.inventoryItem);

        await unitOfWork.InventoryRepository.UpdateAsync(item, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        return new UpdateInventoryItemResult(true);
    }
}


