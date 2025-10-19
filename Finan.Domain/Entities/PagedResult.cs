namespace Finan.Domain.Entities
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasNext { get; set; }

        public PagedResult(List<T> items, int count, int page, int pageSize)
        {
            Items = items;
            TotalCount = count;
            Page = page;
            PageSize = pageSize;
            HasNext = page * pageSize < count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
