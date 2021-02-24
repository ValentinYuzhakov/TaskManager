using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Services.Interfaces;
using TaskManager.Shared;
using TaskManager.Shared.Infos;

namespace TaskManager.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IIdentityService identityService;


        public UserService(IIdentityService identityService)
        {
            this.identityService = identityService;
        }


        public async Task<Guid> Create(UserRegistrationInfo info)
        {
            return await identityService.Register(info);
        }

        public async Task<string> Authorize(UserAuthorizeInfo info)
        {
            return await identityService.Authorize(info);
        }
    }
}