using System.ComponentModel.DataAnnotations;

namespace NorthwindAspNetCore.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string Password { get; set; }
    }
}
