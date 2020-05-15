using Microsoft.AspNetCore.Identity;
using NorthwindAspNetCore.Models;
using System;
using System.Threading.Tasks;

namespace NorthwindAspNetCore.Data
{
    public class NorthwindDataSeed
    {
        private readonly UserManager<SiteUser> _userManager;

        public NorthwindDataSeed(UserManager<SiteUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            const string userName = "admin@northwind.com";
            const string password = "Pa$$w0rd";

            var user = await _userManager.FindByEmailAsync(userName);
            if (user == null)
            {
                user = new SiteUser
                {
                    UserName = userName,
                    Email = userName,
                    FirstName = "John",
                    LastName = "Smith",
                };

                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded) throw new InvalidOperationException("Cannot create default user");
            }

            if (!await _userManager.IsInRoleAsync(user, "Admin"))
            {
                var result = await _userManager.AddToRoleAsync(user, "Admin");
                if (!result.Succeeded) throw new InvalidOperationException("Cannot add role Admin to default user");
            }
        }
    }
}
