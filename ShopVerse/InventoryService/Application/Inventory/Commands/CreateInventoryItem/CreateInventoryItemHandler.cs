namespace Application.Inventory.Commands.CreateInventoryItem;

public class CreateInventoryItemHandler(IUnitOfWork unitOfWork)
    : ICommandHandler<CreateInventoryItemCommand, Result<CreateInventoryItemResult>>
{
    public async Task<Result<CreateInventoryItemResult>> Handle(CreateInventoryItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var productId = ProductId.Of(request.InventoryItem.ProductId);

            var existingItem = await unitOfWork.InventoryRepository
                .GetByProductIdAsync(productId, cancellationToken);
            if (existingItem != null) {
                existingItem.IncreaseQuantity(request.InventoryItem.Quantity);
                await unitOfWork.SaveChangesAsync();
                return Result<CreateInventoryItemResult>.Success(
                   new CreateInventoryItemResult(existingItem.Id.Value),
                   "Quantity updated successfully");
            }
            var itemId = InventoryItemId.New();
            var productid = request.InventoryItem.ProductId;
            var item = new InventoryItem(
                 itemId,
                 request.InventoryItem.Name,
                 request.InventoryItem.Quantity,
                 productId
             );


            await unitOfWork.InventoryRepository.AddAsync(item, cancellationToken);

            await unitOfWork.SaveChangesAsync();
            var result = new CreateInventoryItemResult(itemId.Value);
            return Result<CreateInventoryItemResult>.Success(result, "Item created successfully");

        }
        catch (Exception ex)
        {
        
            return Result<CreateInventoryItemResult>.Fail($"Error creating item: {ex.Message}");
        }
    }
}
