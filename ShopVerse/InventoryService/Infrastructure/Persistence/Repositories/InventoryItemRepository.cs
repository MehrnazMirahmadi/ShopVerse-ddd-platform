namespace Infrastructure.Persistence.Repositories;

public class InventoryItemRepository
    (RepositoryPatternDbContext _db)
    : IInventoryItemRepository
{
    public async Task AddAsync(InventoryItem item, CancellationToken cancellationToken)
    {
        await _db.InventoryItems.AddAsync(item, cancellationToken);
    }

    public async Task<IReadOnlyList<InventoryItem>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.InventoryItems.ToListAsync(cancellationToken);
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
