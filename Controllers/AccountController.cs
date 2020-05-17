using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NorthwindAspNetCore.Models;
using NorthwindAspNetCore.ViewModels;
using System.Threading.Tasks;

namespace NorthwindAspNetCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<SiteUser> _signInManager;

        public AccountController(SignInManager<SiteUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, [FromQuery(Name = "ReturnUrl")] string returnUrl)
        {
            if (!ModelState.IsValid) return View();

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                if (string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password");

                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}