using ShopVerse.BuildingBlocks.Paging;

namespace Infrastructure.Persistence.Repositories;

public class InventoryItemRepository
    (RepositoryPatternDbContext _db)
    : IInventoryItemRepository
{
    public async Task AddAsync(InventoryItem item, CancellationToken cancellationToken)
    {
        await _db.InventoryItems.AddAsync(item, cancellationToken);
    }

    public async Task<PaginationResult<InventoryItem>> GetAllAsync(PaginationRequest paging, CancellationToken cancellationToken)
    {
        var query = _db.InventoryItems.AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((paging.PageNumber - 1) * paging.PageSize)
            .Take(paging.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginationResult<InventoryItem>(paging.PageSize, paging.PageNumber, totalCount, items);
    }


    public async Task<InventoryItem?> GetByIdAsync(InventoryItemId id, CancellationToken cancellationToken)
    {
        return await _db.InventoryItems
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(InventoryItem item, CancellationToken cancellationToken)
    {
        _db.InventoryItems.Update(item);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
