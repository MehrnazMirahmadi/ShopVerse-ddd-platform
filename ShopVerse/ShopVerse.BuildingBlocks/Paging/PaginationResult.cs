namespace ShopVerse.BuildingBlocks.Paging;

public class PaginationResult<T>
{
    public int PageSize { get; init; }
    public int PageNumber { get; init; }
    public int TotalCount { get; init; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public IReadOnlyList<T> Data { get; init; }

    public PaginationResult(int pageSize, int pageNumber, int totalCount, IReadOnlyList<T> data)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
        TotalCount = totalCount;
        Data = data;
    }

    public static PaginationResult<T> Create(int pageSize, int pageNumber, int totalCount, IReadOnlyList<T> data)
    {
        return new PaginationResult<T>(pageSize, pageNumber, totalCount, data);
    }
}
