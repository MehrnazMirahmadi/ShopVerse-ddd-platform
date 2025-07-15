using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Services;

public interface IProductService
{
    Task<ProductId> CreateProductAsync(string name, decimal price, CancellationToken cancellationToken);
    Task<Product?> GetByIdAsync(ProductId id, CancellationToken cancellationToken);
    Task UpdateProductAsync(Product product, CancellationToken cancellationToken);
    Task DeleteProductAsync(ProductId id, CancellationToken cancellationToken);
}

