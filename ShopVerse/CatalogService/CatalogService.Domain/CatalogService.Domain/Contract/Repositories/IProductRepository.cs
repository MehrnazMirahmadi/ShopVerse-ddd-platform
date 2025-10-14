using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using ShopVerse.BuildingBlocks.Paging;
using ShopVerse.BuildingBlocks.Specifications;

namespace Catalog.Domain.Contract.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(ProductId id, CancellationToken cancellationToken = default);
    Task AddAsync(Product product, CancellationToken cancellationToken = default);
    Task UpdateAsync(Product product, CancellationToken cancellationToken = default);
    Task DeleteAsync(Product product, CancellationToken cancellationToken = default);
    Task<PaginationResult<Product>> GetAllProductAsync(BaseSpecification<Product> specification, CancellationToken cancellationToken);
}

