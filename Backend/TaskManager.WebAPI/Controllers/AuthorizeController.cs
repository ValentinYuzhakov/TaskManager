using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManager.Core.Services;
using TaskManager.Core.Services.Interfaces;
using TaskManager.Shared;

namespace TaskManager.WebAPI.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/Authorize")]
    public class AuthorizeController : ControllerBase
    {
        public async Task<UserTokens> Authorize([FromBody] UserAuthorizeInfo request,
            [FromServices] IUserService userService)
        {
            return await userService.Authorize(request);
        }
    }
}
