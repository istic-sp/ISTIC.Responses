using Stick.Core.Pagination.Enums;

namespace Stick.Core.Pagination;

public abstract class PageRequest
{
    private const int MaxPageSize = 50;
    private int _pageSize = 30;

    public int Page { get; set; } = 1;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }
    public SortDirection SortDirection { get; set; }
}
