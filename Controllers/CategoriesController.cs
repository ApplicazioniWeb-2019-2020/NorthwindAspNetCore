using Microsoft.AspNetCore.Mvc;
using NorthwindAspNetCore.Models;
using System.Collections.Generic;

namespace NorthwindAspNetCore.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            var model = GetCategories();
            
            ViewBag.Message = "Hello from Action";
            ViewData["SubTitle"] = "This is a subtitle";

            return View(model);
        }

        private IEnumerable<Category> GetCategories()
        {
            return new[]
            {
                new Category { Id = 1, Name = "Beverages" },
                new Category { Id = 2, Name = "Snacks" },
            };
        }
    }
}