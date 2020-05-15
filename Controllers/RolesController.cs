using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NorthwindAspNetCore.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}