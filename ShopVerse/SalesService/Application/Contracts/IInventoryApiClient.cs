namespace Application.Contracts;

public interface IInventoryApiClient
{
    Task<bool> CheckProductAvailabilityAsync(Guid productId, int quantity);
}

