namespace Application.Inventory.Commands.CreateInventoryItem;

public record CreateInventoryItemCommand(InventoryItemDto InventoryItem)
    : ICommand<CreateInventoryItemResult>;
public record CreateInventoryItemResult(Guid id);

public class CreateInventoryItemCommandValidator : AbstractValidator<CreateInventoryItemCommand>
{
    public CreateInventoryItemCommandValidator()
    {
        RuleFor(x => x.InventoryItem.Name).NotEmpty().WithMessage("Name is required");

    }
}