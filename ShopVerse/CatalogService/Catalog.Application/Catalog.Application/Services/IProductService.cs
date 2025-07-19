using Catalog.Application.Dtos;
using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;

namespace Catalog.Application.Services;

public interface IProductService
{
    Task<ProductId> CreateProductAsync(ProductDto ProductDto, CancellationToken cancellationToken);
    Task<Product?> GetByIdAsync(ProductId id, CancellationToken cancellationToken);
    Task UpdateProductAsync(Product product, UpdateProductDto dto, CancellationToken cancellationToken);
    Task DeleteProductAsync(ProductId id, CancellationToken cancellationToken);
}
