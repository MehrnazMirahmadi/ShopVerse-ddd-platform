
using Application.Services;

namespace Application.Sales.Commands.CreateProduct;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IProductService _productService;

    public CreateProductCommandHandler(IProductService productService)
    {
        _productService = productService;
    }
    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var id = await _productService.CreateProductAsync(request.ProductDto.Name, request.ProductDto.Price, cancellationToken);
            return new CreateProductResult(true, id.Value);
        }
        catch (Exception ex)
        {
            return new CreateProductResult(false, null, ex.Message);
        }
    }
}
