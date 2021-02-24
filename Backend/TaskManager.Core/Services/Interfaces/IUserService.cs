using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Shared;
using TaskManager.Shared.Infos;

namespace TaskManager.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<Guid> Create(UserRegistrationInfo info);
        Task<UserTokens> Authorize(UserAuthorizeInfo info);
    }
}
