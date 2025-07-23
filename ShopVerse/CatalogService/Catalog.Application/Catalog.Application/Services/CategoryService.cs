using AutoMapper;
using Catalog.Application.Dtos;
using Catalog.Domain.Contract;
using Catalog.Domain.Contract.Repositories;
using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;

namespace Catalog.Application.Services;

public class CategoryService
    : ICategoryService
{
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IProductRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;

    }
    public Task<CategoryId> CreateCategoryAsync(CategoryDto categoryDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCategoryAsync(CategoryId id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Category?> GetByIdAsync(CategoryId id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCategoryAsync(Category category, UpdateCategoryDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
