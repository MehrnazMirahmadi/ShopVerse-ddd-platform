using AutoMapper;
using Catalog.Application.Dtos;
using Catalog.Application.Mappings;
using Catalog.Domain.Contract;
using Catalog.Domain.Contract.Repositories;
using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;

namespace Catalog.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IProductRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
       
    }


    public async Task<ProductId> CreateProductAsync(ProductDto ProductDto, CancellationToken cancellationToken)
    {
       
        var product = ProductMapper.FromDto(ProductDto);
        await _repository.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync();
        return product.Id;
    }
    

    public async Task<Product?> GetByIdAsync(ProductId id, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(id, cancellationToken);
    }

    public async Task UpdateProductAsync(Product product, UpdateProductDto dto, CancellationToken cancellationToken)
    {

        var categoryGuid = Guid.Parse(dto.CategoryId);
        var categoryId = CategoryId.Of(categoryGuid);
        product.UpdateDetails(
            dto.Name,
            dto.SmallDescription,
            dto.Slug,
            dto.BasePrice,
            dto.Discount,
            dto.AvailableCount,
            categoryId);

     

        await _repository.UpdateAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync();
    }


    public async Task DeleteProductAsync(ProductId id, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(id, cancellationToken);
        if (product is not null)
        {
            await _repository.DeleteAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync();
        }

    }

}

