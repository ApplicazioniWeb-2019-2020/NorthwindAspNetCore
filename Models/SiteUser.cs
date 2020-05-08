using Microsoft.AspNetCore.Identity;

namespace NorthwindAspNetCore.Models
{
    public class SiteUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
