using Application.Specifications;
using Domain.Contract;
using ShopVerse.BuildingBlocks.Paging;
using ShopVerse.BuildingBlocks.Result;


namespace Application.Sales.Queries.GetOrders;

public record GetOrdersQueryHandler
    (IUnitOfWork unitOfWork)
    : IQueryHandler<GetOrdersQuery, Result<PaginationResult<OrderListDto>>>
{
    public async Task<Result<PaginationResult<OrderListDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var spec = new OrdersSpecification(request.Paging);

        var pagedItems = await unitOfWork.OrderRepository
            .GetAllAsync(spec, cancellationToken);

        if (pagedItems.Data == null || !pagedItems.Data.Any())
        {
            return Result<PaginationResult<OrderListDto>>.Fail("سفارشی موجود نیست");
        }
        var dtoList = pagedItems.Data.Select(o => new OrderListDto
        {
            OrderId = o.Id.Value,
            CustomerId = o.CustomerId.Value,
            Status = (int)o.Status,
            OrderItems = o.OrderItems.Select(oi => new OrderItemListDto
            {
                OrderItemId = oi.Id.Value,
                ProductId = oi.ProductId.Value,
                Quantity = oi.Quantity,
                Price = oi.Price.Amount
            }).ToList()
        }).ToList();

        var result = new PaginationResult<OrderListDto>(
                                                 pagedItems.PageSize,
                                                 pagedItems.PageNumber,
                                                 pagedItems.TotalCount,
                                                 dtoList
                                                   );

        return Result<PaginationResult<OrderListDto>>.Success(result);
    }
}

