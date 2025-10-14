using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using ShopVerse.BuildingBlocks.Paging;
using ShopVerse.BuildingBlocks.Specifications;

namespace Catalog.Domain.Contract.Repositories;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(CategoryId categoryId,CancellationToken cancellationToken = default);
    Task AddAsync(Category category, CancellationToken cancellationToken = default);
    Task UpdateAsync(Category category, CancellationToken cancellationToken = default);
    Task DeleteAsync(Category category, CancellationToken cancellationToken = default);
    Task<PaginationResult<Category>> GetAllAsync(BaseSpecification<Category> specification, CancellationToken cancellationToken = default);
}
