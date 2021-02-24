using System.Collections.Generic;
using System.Security.Claims;

namespace TaskManager.Core.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
    }
}
