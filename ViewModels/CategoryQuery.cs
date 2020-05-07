namespace NorthwindAspNetCore.ViewModels
{
    public class CategoryQuery : Query
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
