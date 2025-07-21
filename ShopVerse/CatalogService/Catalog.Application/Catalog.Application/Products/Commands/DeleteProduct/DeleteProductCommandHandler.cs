using Catalog.Application.Services;
using ShopVerse.BuildingBlocks.CQRS;

namespace Catalog.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler(IProductService productService)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await productService.GetByIdAsync(request.ProductId, cancellationToken);
            if (product is null)
            {
                return DeleteProductResult.Failure("Product not found.");
            }

            await productService.DeleteProductAsync(request.ProductId, cancellationToken);

            return DeleteProductResult.Success(request.ProductId);
        }
        catch (Exception ex)
        {
            // log the exception if you have a logger
            return DeleteProductResult.Failure($"An error occurred while deleting the product: {ex.Message}");
        }
    }
}
