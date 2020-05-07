using NorthwindAspNetCore.Data;
using NorthwindAspNetCore.Models;
using NorthwindAspNetCore.ViewModels;

namespace NorthwindAspNetCore.Controllers.Api
{
    public class CategoriesController : CrudController<NorthwindContext, Category, CategoryQuery>
    {
        public CategoriesController(NorthwindContext context) : base(context)
        { }
    }
}
