using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NorthwindAspNetCore.Infrastructure;
using NorthwindAspNetCore.ViewModels;

namespace NorthwindAspNetCore.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ActionResult<QueryResponse<IdentityRole>>> Get([FromQuery] RoleQuery query = null)
        {
            return await _roleManager.Roles.ToQueryResponse(query);
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<IdentityRole>> Post([FromBody] IdentityRole role)
        {
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return role;
            }
            else
            {
                throw Error.Identity(result.Errors);
            }
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public async Task<ActionResult<IdentityRole>> Put(string id, [FromBody] IdentityRole role)
        {
            var r = await _roleManager.FindByIdAsync(id);
            if (r == null) return NotFound();

            r.Name = role.Name;
            var result = await _roleManager.UpdateAsync(r);

            if (result.Succeeded)
            {
                return r;
            }
            else
            {
                throw Error.Identity(result.Errors);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IdentityRole>> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return role;
            }
            else
            {
                throw Error.Identity(result.Errors);
            }
        }
    }
}
