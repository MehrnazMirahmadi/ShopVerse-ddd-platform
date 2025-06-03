using Domain.Entities;
using ShopVerse.BuildingBlocks.Paging;
using ShopVerse.BuildingBlocks.Specifications;

namespace Application.Specifications;

public class OrdersSpecification : BaseSpecification<Order>
{
    public OrdersSpecification(PaginationRequest paging)
    {
        if (!string.IsNullOrWhiteSpace(paging.FilterName))
        {
            AddCriteria(x => x.OrderName.Value.Contains(paging.FilterName));
        }

        if (paging.SortByQuantityDesc)
        {
            ApplyOrderByDescending(x => x.OrderItems.Count());
        }

        var skip = (paging.PageNumber - 1) * paging.PageSize;
        var take = paging.PageSize;
        ApplyPaging(skip, take);
    }
}
