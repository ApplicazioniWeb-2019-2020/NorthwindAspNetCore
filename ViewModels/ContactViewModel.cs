using System.ComponentModel.DataAnnotations;

namespace NorthwindAspNetCore.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Email obbligatoria")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
