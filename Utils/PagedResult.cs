namespace Utils
{
    public class PagedResult<T>
    {
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<T> Items { get; set; }

        public PagedResult(List<T> items, int totalRecords, int pageNumber, int pageSize)
        {
            TotalRecords = totalRecords;
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            Items = items;
        }
    }
}
