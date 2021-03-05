using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Auth.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.WebAPI.Controllers
{
    [Route("api/Identity")]
    [ApiController]
    public class IdentityController : SystemController
    {
        public IdentityController(UserManager<User> userManager) : base(userManager)
        {
        }

        [HttpPut("confirm-email/{token}")]
        public async Task<IActionResult> ConfirmEmail([FromServices] IIdentityService identityService, string token)
        {
            await identityService.ConfirmEmailAsync(CurrentUserId, token);
            return Ok();
        }
    }
}