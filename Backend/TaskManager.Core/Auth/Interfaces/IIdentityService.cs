using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Models;
using TaskManager.Shared;
using TaskManager.Shared.Infos;
using TaskManager.Shared.ViewModels;

namespace TaskManager.Core.Auth.Interfaces
{
    public interface IIdentityService
    {
        Task<User> Register(UserRegistrationInfo info);
        Task<UserAuthorizeView> Authorize(UserAuthorizeInfo info);
        Task<RefreshResult> RefreshToken();
        Task RevokeRefreshToken();
    }
}
