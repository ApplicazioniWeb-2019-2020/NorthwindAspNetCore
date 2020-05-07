using NorthwindAspNetCore.Data;
using NorthwindAspNetCore.Models;
using NorthwindAspNetCore.ViewModels;

namespace NorthwindAspNetCore.Controllers.Api
{
    public class SuppliersController : CrudController<NorthwindContext, Supplier, SupplierQuery>
    {
        public SuppliersController(NorthwindContext context) : base(context)
        { }
    }
}
