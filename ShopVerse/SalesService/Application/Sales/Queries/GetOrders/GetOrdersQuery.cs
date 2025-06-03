using ShopVerse.BuildingBlocks.Paging;
using ShopVerse.BuildingBlocks.Result;
using System.Collections.Generic;

namespace Application.Sales.Queries.GetOrders;

public record GetOrdersQuery
    (PaginationRequest Paging,
    string? FilterName = null,
    bool SortByQuantityDesc = false)
    :IQuery<Result<PaginationResult<OrderListDto>>>;

public record GetOrdersQueryResult(IReadOnlyList<OrderListDto> Orders);

