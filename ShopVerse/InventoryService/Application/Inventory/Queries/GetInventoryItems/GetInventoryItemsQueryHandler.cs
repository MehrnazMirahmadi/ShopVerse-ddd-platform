using Application.Inventory.Specifications;

namespace Application.Inventory.Queries.GetInventoryItems;

public class GetInventoryItemsQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetInventoryItemsQuery, Result<PaginationResult<InventoryItemDto>>>
{

    public async Task<Result<PaginationResult<InventoryItemDto>>> Handle(GetInventoryItemsQuery request, CancellationToken cancellationToken)
    {
        try
        {
         
            var spec = new InventoryItemSpecification(request.Paging);

            var pagedItems = await unitOfWork.InventoryRepository.GetAllAsync(spec, cancellationToken);

            if (pagedItems.Data == null || !pagedItems.Data.Any())
            {
                return Result<PaginationResult<InventoryItemDto>>.Fail("انبار خالی است");
            }

            var dtoList = pagedItems.Data.Select(i => new InventoryItemDto(i.Id.Value, i.Name, i.Quantity,i.ProductId)).ToList();

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
