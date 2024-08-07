﻿namespace Stick.Core.Pagination;

public class PageResult<T>
{
    public IEnumerable<T> Items { get; set; }

    public PageResult(IEnumerable<T> items) => Items = items;
}
