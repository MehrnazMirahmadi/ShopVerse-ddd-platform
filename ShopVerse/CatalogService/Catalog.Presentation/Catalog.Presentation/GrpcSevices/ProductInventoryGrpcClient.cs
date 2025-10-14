using Catalog.Application.GrpcInterface;
using Inventory.Grpc;

namespace Catalog.Presentation.GrpcSevices;

public class ProductInventoryGrpcClient : IProductInventoryGrpcClient
{
    private readonly ProductInventoryService.ProductInventoryServiceClient _client;

    public ProductInventoryGrpcClient(ProductInventoryService.ProductInventoryServiceClient client)
    {
        _client = client;
    }

    public async Task<Dictionary<Guid, int>> GetAvailableQuantitiesAsync(List<Guid> productIds)
    {
        var request = new ProductInventoryRequest();
        request.ProductIds.AddRange(productIds.Select(id => id.ToString()));

        var response = await _client.GetAvailableQuantitiesAsync(request);

        return response.Items.ToDictionary(
            item => Guid.Parse(item.ProductId),
            item => item.AvailableQuantity
        );
    }
}
