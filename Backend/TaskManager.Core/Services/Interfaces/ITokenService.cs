using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManager.Domain.Models;

namespace TaskManager.Core.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateJwtToken(User user);
        string GenerateRefreshToken();
        Task<RefreshResult> Refresh(string jwtToken, string refreshToken);
        void RevokeRefreshToken(User user, string refreshToken);
    }
}
