namespace NorthwindAspNetCore.ViewModels
{
    public abstract class Query
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string SortField { get; set; }

        public string SortOrder { get; set; }
    }
}
