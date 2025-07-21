using Catalog.Domain.ValueObjects;
using ShopVerse.BuildingBlocks.CQRS;

namespace Catalog.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(ProductId ProductId)
    : ICommand<DeleteProductResult>;

public record DeleteProductResult
{
    public bool IsSuccess { get; init; }
    public string? ErrorMessage { get; init; }
    public ProductId? ProductId { get; init; }

    public static DeleteProductResult Success(ProductId productId) =>
        new() { IsSuccess = true, ProductId = productId };

    public static DeleteProductResult Failure(string errorMessage) =>
        new() { IsSuccess = false, ErrorMessage = errorMessage };
}
