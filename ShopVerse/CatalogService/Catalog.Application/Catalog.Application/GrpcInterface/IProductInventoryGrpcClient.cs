namespace Catalog.Application.GrpcInterface;

public interface IProductInventoryGrpcClient
{
    Task<Dictionary<Guid, int>> GetAvailableQuantitiesAsync(List<Guid> productIds);
}
