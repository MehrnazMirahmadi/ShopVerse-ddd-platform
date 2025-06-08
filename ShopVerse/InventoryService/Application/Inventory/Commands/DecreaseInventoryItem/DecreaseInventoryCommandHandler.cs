using MediatR;

namespace Application.Inventory.Commands.DecreaseInventoryItem;
public class DecreaseInventoryCommandValidator : AbstractValidator<DecreaseInventoryCommand>
{
    public DecreaseInventoryCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}

public class DecreaseInventoryCommandHandler
    (IUnitOfWork unitOfWork)
    : IRequestHandler<DecreaseInventoryCommand, Result<DecreaseInventoryResult>>
{
    public async Task<Result<DecreaseInventoryResult>> Handle(DecreaseInventoryCommand request, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.InventoryRepository.DecreaseQuantityAsync(ProductId.Of(request.ProductId), request.Quantity, cancellationToken);

        return result
            ? Result<DecreaseInventoryResult>.Success(new DecreaseInventoryResult(true))
            : Result<DecreaseInventoryResult>.Fail("Inventory decrease failed: Not enough quantity or item not found.");
    }
}
