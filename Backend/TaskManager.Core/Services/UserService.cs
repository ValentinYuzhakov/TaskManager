using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Auth.Interfaces;
using TaskManager.Core.Services.Interfaces;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Shared;
using TaskManager.Shared.Infos;
using TaskManager.Shared.ViewModels;

namespace TaskManager.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IIdentityService identityService;
        private IUserRepository userRepository;


        public UserService(IIdentityService identityService,
            IUserRepository userRepository)
        {
            this.identityService = identityService;
            this.userRepository = userRepository;
        }


        public async Task<Guid> Create(UserRegistrationInfo info)
        {
            return await identityService.Register(info);
        }

        public async Task<UserAuthorizeView> Authorize(UserAuthorizeInfo info)
        {
            return await identityService.Authorize(info);
        }
    }
}