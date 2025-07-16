using Catalog.Application.Dtos;
using ShopVerse.BuildingBlocks.CQRS;

namespace Catalog.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(ProductDto ProductDto)
    : ICommand<CreateProductResult>;
public record CreateProductResult(bool IsSuccess, Guid? Id = null, string? ErrorMessage = null);
