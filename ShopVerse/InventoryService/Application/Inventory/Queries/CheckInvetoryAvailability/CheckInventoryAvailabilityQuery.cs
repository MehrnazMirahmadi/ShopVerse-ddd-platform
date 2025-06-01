using MediatR;

namespace Application.Inventory.Queries.CheckInvetoryAvailability;

public record CheckInventoryAvailabilityQuery(Guid ProductId, int Quantity) : IRequest<Result<bool>>;


