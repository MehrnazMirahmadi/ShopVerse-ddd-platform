using Catalog.Domain.Entities;
using ShopVerse.BuildingBlocks.Paging;
using ShopVerse.BuildingBlocks.Specifications;

namespace Catalog.Application.Products.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(PaginationRequest paging)
    {
        if (!string.IsNullOrWhiteSpace(paging.FilterName))
            AddCriteria(x => x.Name.Contains(paging.FilterName));
        if (paging.SortByQuantityDesc)
            ApplyOrderByDescending(x => x.Id);
        var skip = (paging.PageNumber - 1) * paging.PageSize;
        var take = paging.PageSize;
        ApplyPaging(skip, take);
    }
}
