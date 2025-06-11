using Finance.Domain.Entities.Invoices;
using ShopVerse.BuildingBlocks.Paging;
using ShopVerse.BuildingBlocks.Specifications;

namespace Finance.Application.Finance.Specifications;

public class InvoiceSpecification : BaseSpecification<Invoice>
{
    public InvoiceSpecification(PaginationRequest paging)
    {
        if (!string.IsNullOrWhiteSpace(paging.FilterName))
            AddCriteria(x => x.InvoiceNumber.Contains(paging.FilterName));

        if (paging.SortByQuantityDesc)
            ApplyOrderByDescending(x => x.TotalAmount);
        else
            ApplyOrderBy(x => x.InvoiceNumber);

        var skip = (paging.PageNumber - 1) * paging.PageSize;
        var take = paging.PageSize;
        ApplyPaging(skip, take);

    }
}
