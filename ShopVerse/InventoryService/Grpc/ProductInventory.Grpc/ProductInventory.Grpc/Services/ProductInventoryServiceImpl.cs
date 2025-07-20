using Domain.Contract.Repositories;
using Domain.ValueObjects;
using Grpc.Core;
using Inventory.Grpc;

namespace ProductInventory.Grpc.Services;

public class ProductInventoryServiceImpl(
    IInventoryItemRepository inventoryItemRepository
) : ProductInventoryService.ProductInventoryServiceBase
{
    public override async Task<ProductInventoryResponse> GetAvailableQuantities(
        ProductInventoryRequest request,
        ServerCallContext context)
    {
        var response = new ProductInventoryResponse();

        foreach (var productIdStr in request.ProductIds)
        {
            if (!Guid.TryParse(productIdStr, out var productGuid))
                continue;

            ProductId productId = ProductId.Of(productGuid);
            var quantity = await inventoryItemRepository.GetProductQuantityAsync(productId, context.CancellationToken);

            response.Items.Add(new ProductInventoryResponseItem
            {
                ProductId = productGuid.ToString(),
                AvailableQuantity = quantity
            });
        }

        return response;
    }
}
