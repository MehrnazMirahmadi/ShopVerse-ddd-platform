using Catalog.Application.Dtos;
using Catalog.Domain.ValueObjects;
using ShopVerse.BuildingBlocks.CQRS;

namespace Catalog.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(ProductId ProductId, UpdateProductDto UpdateDto)
    : ICommand<UpdateProductResult>;

public record UpdateProductResult
{
    public bool IsSuccess { get; init; }
    public string? ErrorMessage { get; init; }
    public ProductId? ProductId { get; init; }

    public static UpdateProductResult Success(ProductId productId) =>
        new() { IsSuccess = true, ProductId = productId };

    public static UpdateProductResult Failure(string errorMessage) =>
        new() { IsSuccess = false, ErrorMessage = errorMessage };
}
