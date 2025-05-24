using Application.Inventory.Specifications;

namespace Application.Inventory.Queries.GetInventoryItems;

public class GetInventoryItemsQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetInventoryItemsQuery, Result<PaginationResult<InventoryItemDto>>>
{


    //public async Task<Result<PaginationResult<InventoryItemDto>>> Handle(GetInventoryItemsQuery request, CancellationToken cancellationToken)
    //{
    //    try
    //    {
    //        // فراخوانی ریپازیتوری با صفحه‌بندی
    //        var pagedItems = await unitOfWork.InventoryRepository.GetAllAsync(request.Paging, cancellationToken);

    //        // تبدیل InventoryItem به InventoryItemDto
    //        var dtoList = pagedItems.Data.Select(i => new InventoryItemDto(
    //            i.Id.Value,
    //            i.Name,
    //            i.Quantity
    //        )).ToList();

    //        // ساخت PaginationResult جدید با dto ها
    //        var paginatedResult = new PaginationResult<InventoryItemDto>(
    //            pagedItems.PageSize,
    //            pagedItems.PageNumber,
    //            pagedItems.TotalCount,
    //            dtoList
    //        );

    //        return Result<PaginationResult<InventoryItemDto>>.Success(paginatedResult);
    //    }
    //    catch (Exception ex)
    //    {
    //        return Result<PaginationResult<InventoryItemDto>>.Fail($"Failed to get inventory items: {ex.Message}");
    //    }
    //}
    public async Task<Result<PaginationResult<InventoryItemDto>>> Handle(GetInventoryItemsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // ساخت specification با criteria و paging و order by دلخواه
            var spec = new InventoryItemSpecification(request.Paging);

            var pagedItems = await unitOfWork.InventoryRepository.GetAllAsync(spec, cancellationToken);

            var dtoList = pagedItems.Data.Select(i => new InventoryItemDto(i.Id.Value, i.Name, i.Quantity)).ToList();

            var paginatedResult = new PaginationResult<InventoryItemDto>(
                pagedItems.PageSize,
                pagedItems.PageNumber,
                pagedItems.TotalCount,
                dtoList);

            return Result<PaginationResult<InventoryItemDto>>.Success(paginatedResult);
        }
        catch (Exception ex)
        {
            return Result<PaginationResult<InventoryItemDto>>.Fail($"Failed to get inventory items: {ex.Message}");
        }
    }

}
