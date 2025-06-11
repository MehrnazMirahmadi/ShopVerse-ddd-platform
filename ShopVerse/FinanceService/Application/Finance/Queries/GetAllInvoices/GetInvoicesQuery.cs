using Finance.Application.Dtos;
using ShopVerse.BuildingBlocks.CQRS;
using ShopVerse.BuildingBlocks.Paging;

namespace Finance.Application.Finance.Queries.GetAllInvoices;

public record GetInvoicesQuery(
     PaginationRequest Paging,
    string? FilterName = null,
    bool SortByQuantityDesc = false)
    : IQuery<Result<PaginationResult<InvoiceItemDto>>>;
public record InvoiceItemsResult(IReadOnlyList<InvoiceItemDto> InvoiceItems);
