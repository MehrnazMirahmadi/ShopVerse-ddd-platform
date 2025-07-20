using Catalog.Application.Dtos;
using ShopVerse.BuildingBlocks.CQRS;
using ShopVerse.BuildingBlocks.Paging;
using ShopVerse.BuildingBlocks.Result;

namespace Catalog.Application.Products.Queries;

public record GetAllProductsQuery(
    PaginationRequest Paging,
    string? FilterName = null,
    bool SortByQuantityDesc = false
)
    : IQuery<Result<PaginationResult<ProductDto>>>;

public record GetAllProductsResult(IReadOnlyList<ProductDto> Products);
