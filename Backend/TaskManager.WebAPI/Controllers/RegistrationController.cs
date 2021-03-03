using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TaskManager.Core.Services.Interfaces;
using TaskManager.Shared.Infos;

namespace TaskManager.WebAPI.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/Registration")]
    public class RegistrationController : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> CreateUser([FromBody] UserRegistrationInfo request,
            [FromServices] IUserService userService)
        {
            return await userService.Create(request);
        }
    }
}
