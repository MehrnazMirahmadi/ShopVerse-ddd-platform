using MediatR;

namespace Application.Inventory.Queries.CheckInvetoryAvailability;


public class CheckInventoryAvailabilityQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CheckInventoryAvailabilityQuery, Result<bool>>
{


    public async Task<Result<bool>> Handle(CheckInventoryAvailabilityQuery request, CancellationToken cancellationToken)
    {
        var available = await unitOfWork.InventoryRepository.GetAvailableQuantityAsync(ProductId.Of(request.ProductId), request.Quantity);
        return Result<bool>.Success(available);
    }
}