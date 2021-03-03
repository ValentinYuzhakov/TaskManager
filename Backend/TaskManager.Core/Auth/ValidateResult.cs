using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TaskManager.Core.Auth
{
    public class ValidateResult
    {
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
        public JwtSecurityToken Token { get; set; }
        public bool IsValid { get; set; }
    }
}