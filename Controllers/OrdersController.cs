using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NorthwindAspNetCore.Controllers
{
    public class OrdersController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}