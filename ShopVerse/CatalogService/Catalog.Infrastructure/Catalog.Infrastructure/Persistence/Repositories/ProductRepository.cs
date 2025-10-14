using Catalog.Domain.Contract.Repositories;
using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using Catalog.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using ShopVerse.BuildingBlocks.Paging;
using ShopVerse.BuildingBlocks.Specifications;

namespace Catalog.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly CatalogDbContext _context;

    public ProductRepository(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(ProductId id, CancellationToken cancellationToken = default)
    {
        return await _context.Products.FindAsync(new object[] { id }, cancellationToken);
    }

    public Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        _context.Products.Update(product);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Product product, CancellationToken cancellationToken = default)
    {
        _context.Products.Remove(product);
        return Task.CompletedTask;
    }

    public async Task<PaginationResult<Product>> GetAllProductAsync(BaseSpecification<Product> specification, CancellationToken cancellationToken)
    {
      var query = _context.Products.AsQueryable();
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

        return new PaginationResult<Product>(
            specification.Take ?? totalCount,
            specification.Skip.HasValue ? (specification.Skip.Value / (specification.Take ?? totalCount) + 1) : 1,
            totalCount,
            items);
    }
}