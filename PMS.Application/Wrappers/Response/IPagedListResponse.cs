namespace PMS.Application.Wrappers.Response
{
    public interface IPagedListResponse<T>
    {
        int PageIndex { get; }
        int PageSize { get; }

        int PageCount { get; }
        int RowCount { get; }

        string? ActiveFilter { get; }
        string? ActiveOrderBy { get; }

        int FirstRowOnPage { get; }
        int LastRowOnPage { get; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < PageCount;
        public string? SearchText { get; set; }

        IEnumerable<T> Data { get; set; }
    }
}
