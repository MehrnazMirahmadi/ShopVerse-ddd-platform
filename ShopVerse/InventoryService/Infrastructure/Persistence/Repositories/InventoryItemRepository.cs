using ShopVerse.BuildingBlocks.Paging;
using ShopVerse.BuildingBlocks.Specifications;

namespace Infrastructure.Persistence.Repositories;

public class InventoryItemRepository
    (InventoryDbContext _db)
    : IInventoryItemRepository
{
    public async Task AddAsync(InventoryItem item, CancellationToken cancellationToken)
    {
        await _db.InventoryItems.AddAsync(item, cancellationToken);
    }
    public async Task<PaginationResult<InventoryItem>> GetAllAsync(
    BaseSpecification<InventoryItem> specification,
    CancellationToken cancellationToken)
    {
        var query = _db.InventoryItems.AsQueryable();

        if (specification.Criteria != null)
            query = query.Where(specification.Criteria);


        foreach (var include in specification.Includes)
            query = query.Include(include);

        if (specification.OrderBy != null)
            query = query.OrderBy(specification.OrderBy);
        else if (specification.OrderByDescending != null)
            query = query.OrderByDescending(specification.OrderByDescending);

        var totalCount = await query.CountAsync(cancellationToken);


        if (specification.Skip.HasValue && specification.Take.HasValue)
            query = query.Skip(specification.Skip.Value).Take(specification.Take.Value);

        var items = await query.AsNoTracking().ToListAsync(cancellationToken);

        return new PaginationResult<InventoryItem>(
            specification.Take ?? totalCount,
            specification.Skip.HasValue ? (specification.Skip.Value / (specification.Take ?? totalCount) + 1) : 1,
            totalCount,
            items);
    }

    public async Task<InventoryItem?> GetByIdAsync(InventoryItemId id, CancellationToken cancellationToken)
    {
        return await _db.InventoryItems
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<PaginationResult<InventoryItem>> GetBySpecificationAsync(
      BaseSpecification<InventoryItem> spec,
      CancellationToken cancellationToken)
    {
        var query = _db.InventoryItems.AsQueryable();

        // اعمال Criteria (فیلترها)
        if (spec.Criteria != null)
            query = query.Where(spec.Criteria);

        // اعمال Includes (شامل روابط)
        foreach (var include in spec.Includes)
            query = query.Include(include);

        // اعمال OrderBy و OrderByDescending
        if (spec.OrderBy != null)
            query = query.OrderBy(spec.OrderBy);
        else if (spec.OrderByDescending != null)
            query = query.OrderByDescending(spec.OrderByDescending);

        var totalCount = await query.CountAsync(cancellationToken);

        // اعمال پیجینگ
        if (spec.Skip.HasValue)
            query = query.Skip(spec.Skip.Value);
        if (spec.Take.HasValue)
            query = query.Take(spec.Take.Value);

        var data = await query.ToListAsync(cancellationToken);

        // محاسبه PageNumber بر اساس skip و take
        int pageNumber = 1;
        if (spec.Skip.HasValue && spec.Take.HasValue && spec.Take != 0)
            pageNumber = (spec.Skip.Value / spec.Take.Value) + 1;

        return new PaginationResult<InventoryItem>(
            spec.Take ?? totalCount,
            pageNumber,
            totalCount,
            data);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(InventoryItem item, CancellationToken cancellationToken)
    {
        _db.InventoryItems.Update(item);
    }
    public async Task<bool> GetAvailableQuantityAsync(ProductId productId, int quantity)
    {

        var inventoryItem = await _db.InventoryItems
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ProductId == productId);

        if (inventoryItem is null)
            return false;

        return inventoryItem != null && inventoryItem.Quantity >= quantity;
    }

    public async Task<InventoryItem?> GetByProductIdAsync(ProductId productId, CancellationToken cancellationToken = default)
    {
        return await _db.InventoryItems
            .FirstOrDefaultAsync(x => x.ProductId == productId, cancellationToken);
    }

    public async Task<bool> DecreaseQuantityAsync(ProductId productId, int quantity, CancellationToken cancellationToken = default)
    {
        var item = await _db.InventoryItems.FirstOrDefaultAsync(x => x.ProductId == productId, cancellationToken);

        if (item == null) return false;

        try
        {
            item.DecreaseQuantity(quantity);
            _db.InventoryItems.Update(item);
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }

    public Task<int> GetProductQuantityAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        return _db.InventoryItems
            .Where(x => x.ProductId == productId)
            .Select(x => x.Quantity)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
