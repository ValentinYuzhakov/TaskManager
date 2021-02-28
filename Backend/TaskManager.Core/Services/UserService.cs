using System;
using System.Threading.Tasks;
using TaskManager.Core.Auth.Interfaces;
using TaskManager.Core.Services.Interfaces;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.Shared;
using TaskManager.Shared.Infos;
using TaskManager.Shared.ViewModels;

namespace TaskManager.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IIdentityService identityService;
        private readonly IUserRepository userRepository;
        private readonly ITaskFolderService taskFolderService;


        public UserService(IIdentityService identityService,
            IUserRepository userRepository, ITaskFolderService taskFolderService)
        {
            this.identityService = identityService;
            this.userRepository = userRepository;
            this.taskFolderService = taskFolderService;
        }


        public async Task<Guid> Create(UserRegistrationInfo info)
        {
            var user = await identityService.Register(info);
            await InitializeSystemFolders(user);
            return user.Id;
        }

        public async Task<UserAuthorizeView> Authorize(UserAuthorizeInfo info)
        {
            return await identityService.Authorize(info);
        }

        public async Task InitializeSystemFolders(User user)
        {
            var systemFolders = await taskFolderService.CreateSystemFolders();
            user.TaskFolders.AddRange(systemFolders);
            await userRepository.UpdateAsync(user);
        }
    }
}