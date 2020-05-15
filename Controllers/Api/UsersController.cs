using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NorthwindAspNetCore.Infrastructure;
using NorthwindAspNetCore.Models;
using NorthwindAspNetCore.ViewModels;

namespace NorthwindAspNetCore.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<SiteUser> _userManager;

        public UsersController(UserManager<SiteUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<QueryResponse<SiteUser>>> Get([FromQuery] SiteUserQuery query = null)
        {
            return await _userManager.Users.ToQueryResponse(query);
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<SiteUser>> Post([FromBody] SiteUserViewModel user)
        {
            var siteUser = new SiteUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(siteUser);
            if (result.Succeeded)
            {
                result = await _userManager.ChangePasswordAsync(siteUser, null, user.Password);

                if (result.Succeeded)
                {
                    return siteUser;
                }
                else
                {
                    throw Error.Identity(result.Errors);
                }
            }
            else
            {
                throw Error.Identity(result.Errors);
            }
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public async Task<ActionResult<SiteUser>> Put(string id, [FromBody] SiteUserViewModel user)
        {
            var siteUser = await _userManager.FindByIdAsync(id);
            if (siteUser == null) return NotFound();

            siteUser.UserName = user.UserName;
            siteUser.Email = user.Email;
            siteUser.PhoneNumber = user.PhoneNumber;

            var result = await _userManager.UpdateAsync(siteUser);

            if (result.Succeeded)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(siteUser);
                result = await _userManager.ResetPasswordAsync(siteUser, token, user.Password);

                if (result.Succeeded)
                {
                    return siteUser;
                }
                else
                {
                    throw Error.Identity(result.Errors);
                }
            }
            else
            {
                throw Error.Identity(result.Errors);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SiteUser>> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return user;
            }
            else
            {
                throw Error.Identity(result.Errors);
            }
        }
    }
}
