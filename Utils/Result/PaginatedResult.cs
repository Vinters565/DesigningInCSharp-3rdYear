namespace SchedulePlanner.Utils.Result;

public class PaginatedResult<T> : Result<List<T>>
{
    public List<T> Items => Value;
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public PaginatedResult(List<T> items, int totalCount, int pageNumber, int pageSize) : base(items)
    {
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}