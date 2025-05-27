namespace Application.GrpcInterface;

public interface IInventoryServiceClient
{
    Task<bool> IsProductAvailableAsync(Guid productId, int quantity);
}

