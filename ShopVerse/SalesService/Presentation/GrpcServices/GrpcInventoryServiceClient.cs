using Application.GrpcInterface;
using InventoryGrpc;

namespace Presentation.GrpcServices;
public class GrpcInventoryServiceClient : IInventoryServiceClient
{
    private readonly InventoryService.InventoryServiceClient _client;

    public GrpcInventoryServiceClient(InventoryService.InventoryServiceClient client)
    {
        _client = client;
    }

    public async Task<bool> IsProductAvailableAsync(Guid productId, int quantity)
    {
       
        var request = new CheckStockRequest { ProductId = productId.ToString(), Quantity = quantity };
        var response = await _client.CheckStockAsync(request);
        return response.IsAvailable;
    }
}

