namespace ShopVerse.BuildingBlocks.Paging;

public class PaginationRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public string? FilterName { get; set; } 
    public bool SortByQuantityDesc { get; set; } = false; 
}

