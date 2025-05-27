using ShopVerse.BuildingBlocks.Specifications;

namespace Domain.Contract.Repositories;

public interface IInventoryItemRepository
{
    Task<InventoryItem?> GetByIdAsync(InventoryItemId id, CancellationToken cancellationToken);
    Task<PaginationResult<InventoryItem>> GetAllAsync(BaseSpecification<InventoryItem> specification, CancellationToken cancellationToken);
    Task AddAsync(InventoryItem item, CancellationToken cancellationToken);
    Task Update(InventoryItem item, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task <bool> GetAvailableQuantityAsync(ProductId productId,int quntity);
}
