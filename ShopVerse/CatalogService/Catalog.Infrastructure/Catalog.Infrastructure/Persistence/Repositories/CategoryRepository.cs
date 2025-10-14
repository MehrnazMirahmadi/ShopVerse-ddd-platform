using Catalog.Domain.Contract.Repositories;
using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using ShopVerse.BuildingBlocks.Paging;
using ShopVerse.BuildingBlocks.Specifications;

namespace Catalog.Infrastructure.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository
{
    public Task AddAsync(Category category, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Category category, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<PaginationResult<Category>> GetAllAsync(BaseSpecification<Category> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Category?> GetByIdAsync(CategoryId categoryId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Category category, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
