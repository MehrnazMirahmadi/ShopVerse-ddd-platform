namespace Application.Inventory.Commands.CreateInventoryItem;

public class CreateInventoryItemHandler(IUnitOfWork unitOfWork)
    : ICommandHandler<CreateInventoryItemCommand, Result<CreateInventoryItemResult>>
{
    public async Task<Result<CreateInventoryItemResult>> Handle(CreateInventoryItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var itemId = InventoryItemId.New();

            var item = new InventoryItem(
                itemId,
                request.InventoryItem.Name,
                request.InventoryItem.Quantity
            );


            await unitOfWork.InventoryRepository.AddAsync(item, cancellationToken);

            await unitOfWork.SaveChangesAsync();
            var result = new CreateInventoryItemResult(itemId.Value);
            return Result<CreateInventoryItemResult>.Success(result, "Item created successfully");

        }
        catch (Exception ex)
        {
            // اگر خطایی پیش اومد، پیام مناسب رو برگردون
            return Result<CreateInventoryItemResult>.Fail($"Error creating item: {ex.Message}");
        }
    }
}
