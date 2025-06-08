namespace Application.Inventory.Commands.DecreaseInventoryItem;


public record DecreaseInventoryCommand(Guid ProductId, int Quantity)
    : ICommand<Result<DecreaseInventoryResult>>;

public record DecreaseInventoryResult(bool IsSuccess);
