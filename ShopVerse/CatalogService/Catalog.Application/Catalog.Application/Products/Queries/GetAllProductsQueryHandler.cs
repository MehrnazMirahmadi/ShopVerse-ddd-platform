using Catalog.Application.Dtos;
using Catalog.Application.GrpcInterface;
using Catalog.Application.Products.Specifications;
using Catalog.Domain.Contract.Repositories;
using ShopVerse.BuildingBlocks.CQRS;
using ShopVerse.BuildingBlocks.Paging;
using ShopVerse.BuildingBlocks.Result;

namespace Catalog.Application.Products.Queries;

public class GetAllProductsQueryHandler
    : IQueryHandler<GetAllProductsQuery, Result<PaginationResult<ProductDto>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductInventoryGrpcClient _inventoryGrpcClient;

    public GetAllProductsQueryHandler(
        IProductRepository productRepository,
        IProductInventoryGrpcClient inventoryGrpcClient)
    {
        _productRepository = productRepository;
        _inventoryGrpcClient = inventoryGrpcClient;
    }

    public async Task<Result<PaginationResult<ProductDto>>> Handle(
        GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var spec = new ProductSpecification(request.Paging);
            var productsResult = await _productRepository.GetAllProductAsync(spec, cancellationToken);

            // Convert ProductId to Guid
            var productIds = productsResult.Data.Select(p => p.Id.Value).ToList();
            var inventoryDict = await _inventoryGrpcClient.GetAvailableQuantitiesAsync(productIds);

            var productDtos = productsResult.Data.Select(p => new ProductDto
            {
                Id = p.Id.ToString(),
                Name = p.Name,
                SmallDescription = p.SmallDescription,
                Slug = p.Slug,
                BasePrice = p.BasePrice,
                Discount = p.Discount,
                AvailableCount = inventoryDict.TryGetValue(p.Id.Value, out var count) ? count : 0,
                CategoryId = p.CategoryId.ToString(),
                Features = p.Features.Select(f => new ProductFeatureDto
                {
                    FeatureName = f.FeatureId.ToString(),
                    FeatureValue = f.FeatureValue
                }).ToList(),
                Media = p.Media.Select(m => new ProductMediaDto
                {
                    Url = m.Url,
                    MediaType = m.MediaTypeId.ToString()
                }).ToList()
            }).ToList();

            var pagedResult = new PaginationResult<ProductDto>(
                productsResult.PageSize,
                productsResult.PageNumber,
                productsResult.TotalCount,
                productDtos
            );

            return Result<PaginationResult<ProductDto>>.Success(pagedResult);
        }
        catch (Exception ex)
        {

            return Result<PaginationResult<ProductDto>>.Fail(ex.Message);
        }
    }
}
