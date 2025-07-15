using Domain.Contract;
using Domain.Contract.Repositories;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IProductRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

 
    public async Task<ProductId> CreateProductAsync(string name, decimal price, CancellationToken cancellationToken)
    {
        var product = Product.Create(ProductId.NewId(), name, price);
        await _repository.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync();
        return product.Id;
    }

    public async Task<Product?> GetByIdAsync(ProductId id, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(id, cancellationToken);
    }

    public async Task UpdateProductAsync(Product product, CancellationToken cancellationToken)
    {
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
