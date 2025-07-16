using Catalog.Application.Services;
using ShopVerse.BuildingBlocks.CQRS;

namespace Catalog.Application.Products.Commands.CreateProduct;
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
            // Pass the whole DTO and cancellation token from request
            var id = await _productService.CreateProductAsync(request.ProductDto, cancellationToken);

            return new CreateProductResult(true, id.Value);
        }
        catch (Exception ex)
        {
            // Optionally log the exception here
            return new CreateProductResult(false, null, ex.Message);
        }
    }
}
