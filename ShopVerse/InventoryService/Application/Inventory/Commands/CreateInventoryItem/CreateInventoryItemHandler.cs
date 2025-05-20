namespace Application.Inventory.Commands.CreateInventoryItem;

public class CreateInventoryItemHandler(IUnitOfWork unitOfWork)
    : ICommandHandler<CreateInventoryItemCommand, CreateInventoryItemResult>
{
    public async Task<CreateInventoryItemResult> Handle(CreateInventoryItemCommand request, CancellationToken cancellationToken)
    {

        var itemId = InventoryItemId.New();

        var item = new InventoryItem(
            itemId,
            request.InventoryItem.Name,
            request.InventoryItem.Quantity
        );


        await unitOfWork.InventoryRepository.AddAsync(item, cancellationToken);

        await unitOfWork.SaveChangesAsync();

        return new CreateInventoryItemResult(itemId.Value);
    }
}
