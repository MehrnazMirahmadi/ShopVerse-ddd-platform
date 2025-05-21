namespace Application.Inventory.Queries.GetInventoryItems;

public class GetInventoryItemsQueryHandler : IQueryHandler<GetInventoryItemsQuery, Result<PaginationResult<InventoryItemDto>>>
{
    private readonly IUnitOfWork unitOfWork;

    public GetInventoryItemsQueryHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<PaginationResult<InventoryItemDto>>> Handle(GetInventoryItemsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // فراخوانی ریپازیتوری با صفحه‌بندی
            var pagedItems = await unitOfWork.InventoryRepository.GetAllAsync(request.Paging, cancellationToken);

            // تبدیل InventoryItem به InventoryItemDto
            var dtoList = pagedItems.Data.Select(i => new InventoryItemDto(
                i.Id.Value,
                i.Name,
                i.Quantity
            )).ToList();

            // ساخت PaginationResult جدید با dto ها
            var paginatedResult = new PaginationResult<InventoryItemDto>(
                pagedItems.PageSize,
                pagedItems.PageNumber,
                pagedItems.TotalCount,
                dtoList
            );

            return Result<PaginationResult<InventoryItemDto>>.Success(paginatedResult);
        }
        catch (Exception ex)
        {
            return Result<PaginationResult<InventoryItemDto>>.Fail($"Failed to get inventory items: {ex.Message}");
        }
    }
}
