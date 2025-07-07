using Domain.Contract.Repositories;
using Domain.ValueObjects;
using Grpc.Core;
using InventoryGrpc;
//

namespace Inventory.Grpc.Services;

public class InventoryServiceImpl(IInventoryItemRepository inventoryRepository) : InventoryService.InventoryServiceBase
{
    public override async Task<CheckStockResponse> CheckStock(CheckStockRequest request, ServerCallContext context)
    {
        if (!Guid.TryParse(request.ProductId, out var productGuid))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid ProductId format"));
        }

        ProductId productId = ProductId.Of(productGuid);

        bool isAvailable = await inventoryRepository.GetAvailableQuantityAsync(productId, request.Quantity);

        return new CheckStockResponse { IsAvailable = isAvailable };

    }

}

