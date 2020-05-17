using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NorthwindAspNetCore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}