using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Models;
using TaskManager.Shared;
using TaskManager.Shared.Infos;
using TaskManager.Shared.ViewModels;

namespace TaskManager.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<Guid> Create(UserRegistrationInfo info);
        Task<UserAuthorizeView> Authorize(UserAuthorizeInfo info);
        Task InitializeSystemFolders(User user);
    }
}
