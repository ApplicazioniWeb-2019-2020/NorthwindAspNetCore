using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
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
        static readonly object Empty = new object();

        private readonly UserManager<SiteUser> _userManager;
        private readonly IMapper _mapper;

        public UsersController(UserManager<SiteUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<QueryResponse<SiteUserViewModel>>> Get([FromQuery] SiteUserQuery query = null)
        {
            var users = await _userManager.Users.ToQueryResponse(query);
            var userWithRoles = await GetRolesAsync(users.Data).ToListAsync();

            return new QueryResponse<SiteUserViewModel>
            {
                ItemsCount = users.ItemsCount,
                Data = userWithRoles,
            };
        }

        [HttpPost]
        public async Task<ActionResult<SiteUserViewModel>> Post([FromBody] SiteUserViewModel userViewModel)
        {
            // E' necessario assegnare un ID al nuovo utente. L'ID è un GUID (https://it.wikipedia.org/wiki/GUID).
            userViewModel.Id = Guid.NewGuid().ToString();

            // Converto il tipo da SiteUserViewModel a SiteUser.
            var user = _mapper.Map<SiteUserViewModel, SiteUser>(userViewModel);

            await TryAsync(() => _userManager.CreateAsync(user));
            await AssignRolesAsync(user, userViewModel.Roles);

            return userViewModel;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SiteUserViewModel>> Put(string id, [FromBody] SiteUserViewModel userViewModel)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user = _mapper.Map(userViewModel, user);

            await TryAsync(() => _userManager.UpdateAsync(user));
            await AssignRolesAsync(user, userViewModel.Roles);

            return userViewModel;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SiteUserViewModel>> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            await TryAsync(() => _userManager.DeleteAsync(user));

            return _mapper.Map<SiteUser, SiteUserViewModel>(user);
        }

        [HttpPost("{id}/changepassword")]
        public async Task<ActionResult<object>> ChangePassword(string id, [FromBody] ChangePasswordViewModel model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await TryAsync(() => _userManager.ResetPasswordAsync(user, token, model.Password));

            return Empty;
        }

        async Task AssignRolesAsync(SiteUser user, string roles)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles != null && userRoles.Any())
            {
                await TryAsync(() => _userManager.RemoveFromRolesAsync(user, userRoles));
            }

            userRoles = roles.Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (userRoles != null && userRoles.Any())
            {
                await TryAsync(() => _userManager.AddToRolesAsync(user, userRoles));
            }
        }

        async Task TryAsync(Func<Task<IdentityResult>> func)
        {
            var result = await func();
            if (!result.Succeeded) throw Error.Identity(result.Errors);
        }

        async IAsyncEnumerable<SiteUserViewModel> GetRolesAsync(IEnumerable<SiteUser> users)
        {
            foreach (var user in users ?? Enumerable.Empty<SiteUser>())
            {
                var roles = await _userManager.GetRolesAsync(user);

                //var userViewModel = new SiteUserViewModel
                //{
                //    Id = user.Id,
                //    UserName = user.UserName,
                //    FirstName = user.FirstName,
                //    LastName = user.LastName,
                //};

                var userViewModel = _mapper.Map<SiteUser, SiteUserViewModel>(user);
                userViewModel.Roles = string.Join(',', roles);

                yield return userViewModel;
            }
        }
    }
}
