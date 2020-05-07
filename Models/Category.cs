namespace NorthwindAspNetCore.Models
{
    public class Category : IHasId
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
