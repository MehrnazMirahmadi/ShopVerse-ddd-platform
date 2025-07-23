using Catalog.Application.Dtos;
using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;

namespace Catalog.Application.Services;

public interface ICategoryService
{
    Task<CategoryId> CreateCategoryAsync(CategoryDto categoryDto, CancellationToken cancellationToken);
    Task<Category?> GetByIdAsync(CategoryId id, CancellationToken cancellationToken);
    Task UpdateCategoryAsync(Category category, UpdateCategoryDto dto, CancellationToken cancellationToken);
    Task DeleteCategoryAsync(CategoryId id, CancellationToken cancellationToken);
}
