namespace PMS.Application.Wrappers.Response
{
    public class PagedListResponse<TDto> : IPagedListResponse<TDto>
    {
        public PagedListResponse(ListPagedQuery<TDto> queryModel, int rowCount, IEnumerable<TDto> results)
        {
            Data = results;

            PageIndex = queryModel.PageIndex;
            PageSize = queryModel.PageSize;
            ActiveOrderBy = queryModel.OrderBy;
            ActiveOrderDirection = queryModel.OrderDirection;
            ActiveFilter = queryModel.Filter;
            SearchText = queryModel.SearchText;
            
            RowCount = rowCount;
            PageCount = (int)Math.Ceiling((double)rowCount / PageSize);
        }

        public PagedListResponse(ListPagedQuery<TDto> queryModel, IEnumerable<TDto> results)//, IQueryable<TDto> results)
        {
            Data = results;

            PageIndex = queryModel.PageIndex;
            PageSize = queryModel.PageSize;
            ActiveOrderBy = queryModel.OrderBy;
            ActiveOrderDirection = queryModel.OrderDirection;
            ActiveFilter = queryModel.Filter;
            SearchText = queryModel.SearchText;

            RowCount = results.Count();
            PageCount = (int)Math.Ceiling((double)RowCount / PageSize);
        }

        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }

        public int PageCount { get; private set; }
        public int RowCount { get; private set; }

        public string? ActiveFilter { get; private set; }
        public string? ActiveOrderBy { get; private set; }
        public string? ActiveOrderDirection { get; private set; }
        public string? SearchText { get; set; }

        public int FirstRowOnPage => RowCount <= 0 ? 0 : ((PageIndex - 1) * PageSize) + 1;
        public int LastRowOnPage => Math.Min(PageIndex * PageSize, RowCount);

        public IEnumerable<TDto> Data { get; set; } = new List<TDto>();

    }

}
