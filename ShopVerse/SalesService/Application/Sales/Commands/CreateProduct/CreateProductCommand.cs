namespace Application.Sales.Commands.CreateProduct;

public record CreateProductCommand(ProductDto ProductDto)
    : ICommand<CreateProductResult>;
public record CreateProductResult(bool IsSuccess, Guid? Id = null, string? ErrorMessage = null);