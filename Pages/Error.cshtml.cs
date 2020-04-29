using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NorthwindAspNetCore.Pages
{
    public class ErrorModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "This is a message from OnGet";
        }
    }
}