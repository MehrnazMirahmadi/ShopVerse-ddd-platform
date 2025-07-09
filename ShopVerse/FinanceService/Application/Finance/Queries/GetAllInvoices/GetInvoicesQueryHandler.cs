using Finance.Application.Dtos;
using Finance.Application.Finance.Specifications;
using ShopVerse.BuildingBlocks.Paging;

namespace Finance.Application.Finance.Queries.GetAllInvoices;

public class GetInvoicesQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetInvoicesQuery, Result<PaginationResult<InvoiceItemDto>>>
{
    public async Task<Result<PaginationResult<InvoiceItemDto>>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new InvoiceSpecification(request.Paging);
            var pagedItems = await unitOfWork.InvoiceRepository.GetAllAsync(spec, cancellationToken);
            if (pagedItems.Data == null || !pagedItems.Data.Any())
            {
                return Result<PaginationResult<InvoiceItemDto>>.Fail("فاکتوری موجود نیست");
            }
            var dtoList = pagedItems.Data.Select(i => new InvoiceItemDto(i.Id, i.InvoiceNumber, i.TotalAmount)).ToList();

            var paginatedResult = new PaginationResult<InvoiceItemDto>(
                pagedItems.PageSize,
                pagedItems.PageNumber,
                pagedItems.TotalCount,
                dtoList);

            return Result<PaginationResult<InvoiceItemDto>>.Success(paginatedResult);
        }
        catch (Exception ex)
        {
            return Result<PaginationResult<InvoiceItemDto>>.Fail($"Failed to get inventory items: {ex.Message}");
        }
    }
}
