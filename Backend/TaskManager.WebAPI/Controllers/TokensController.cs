using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManager.Core;
using TaskManager.Core.Auth.Interfaces;
using TaskManager.Core.Services.Interfaces;

namespace TaskManager.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Tokens")]
    public class TokensController : ControllerBase
    {
        private readonly IIdentityService identityService;


        public TokensController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }


        [AllowAnonymous]
        [HttpPut("refresh-token")]
        public async Task<RefreshResult> RefreshToken()
        {
            return await identityService.RefreshToken();
        }

        [HttpPut("revoke-token")]
        public async Task<IActionResult> RevokeRefreshToken()
        {
            await identityService.RevokeRefreshToken();
            return Ok();
        }
    }
}
