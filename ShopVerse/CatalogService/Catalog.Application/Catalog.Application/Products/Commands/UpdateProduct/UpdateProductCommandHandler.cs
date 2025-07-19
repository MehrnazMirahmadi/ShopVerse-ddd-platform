using Catalog.Application.Services;
using ShopVerse.BuildingBlocks.CQRS;

namespace Catalog.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IProductService _productService;
    public UpdateProductCommandHandler(IProductService productService)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }
    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productService.GetByIdAsync(request.ProductId, cancellationToken);
        if (product == null)
            return UpdateProductResult.Failure("Product not found");

        await _productService.UpdateProductAsync(product, request.UpdateDto, cancellationToken);
        return UpdateProductResult.Success(request.ProductId);
    }

}

