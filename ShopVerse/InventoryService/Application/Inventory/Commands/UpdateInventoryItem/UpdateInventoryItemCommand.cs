namespace Application.Inventory.Commands.UpdateInventoryItem;

public record UpdateInventoryItemCommand(InventoryItemDto inventoryItem)
    :ICommand<UpdateInventoryItemResult>;
public record UpdateInventoryItemResult(bool IsSuccess);

public class UpdateInventoryItemCommandValidator : AbstractValidator<UpdateInventoryItemCommand>
{
    public UpdateInventoryItemCommandValidator()
    {
        RuleFor(x => x.inventoryItem.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.inventoryItem.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.inventoryItem.Quantity).NotEmpty().WithMessage("Quantity is required");
    }
}
