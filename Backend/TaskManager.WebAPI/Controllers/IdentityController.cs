using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TaskManager.Core.Auth.Interfaces;
using TaskManager.Core.Services.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.Shared;
using TaskManager.Shared.Infos;
using TaskManager.Shared.ViewModels;

namespace TaskManager.WebAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/Identity")]
    public class IdentityController : SystemController
    {
        public IdentityController(UserManager<User> userManager) : base(userManager) { }


        [HttpPost("registration")]
        public async Task<Guid> Register([FromBody] UserRegistrationInfo request,
            [FromServices] IUserService userService)
        {
            return await userService.Create(request);
        }

        [HttpPost("authorization")]
        public async Task<UserAuthorizeView> Authorize([FromBody] UserAuthorizeInfo request,
            [FromServices] IUserService userService)
        {
            return await userService.Authorize(request);
        }

        [HttpPut("confirm-email/{token}")]
        public async Task<IActionResult> ConfirmEmail([FromServices] IIdentityService identityService, string token)
        {
            await identityService.ConfirmEmailAsync(CurrentUserId, token);
            return Ok();
        }
    }
}