using ShopVerse.BuildingBlocks.Specifications;

namespace Application.Inventory.Specifications;

public class InventoryItemSpecification : BaseSpecification<InventoryItem>
{
    public InventoryItemSpecification(PaginationRequest paging)
    {
           if (!string.IsNullOrWhiteSpace(paging.FilterName))
            AddCriteria(x => x.Name.Contains(paging.FilterName)); 

          if (paging.SortByQuantityDesc)
            ApplyOrderByDescending(x => x.Quantity);
        else
            ApplyOrderBy(x => x.Name);

        var skip = (paging.PageNumber - 1) * paging.PageSize;
        var take = paging.PageSize;
        ApplyPaging(skip, take);
    }
}
