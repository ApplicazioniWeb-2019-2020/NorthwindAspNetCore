using NorthwindAspNetCore.Data;
using NorthwindAspNetCore.Models;
using NorthwindAspNetCore.ViewModels;

namespace NorthwindAspNetCore.Controllers.Api
{
    public class ProductsController : CrudController<NorthwindContext, Product, ProductQuery>
    {
        public ProductsController(NorthwindContext context) : base(context)
        { }
    }
}
