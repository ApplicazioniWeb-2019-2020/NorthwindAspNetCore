using System.ComponentModel.DataAnnotations;

namespace NorthwindAspNetCore.ViewModels
{
    public class SiteUserViewModel
    {
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
