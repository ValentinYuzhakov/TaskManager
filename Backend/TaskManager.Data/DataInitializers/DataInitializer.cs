using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data.DataInitializers.Interfaces;
using TaskManager.Data.DataInitializers.Options;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Models;

namespace TaskManager.Data.DataInitializers
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ITaskFolderRepository taskFolderRepository;
        private readonly RolesOptions rolesOptions;
        private readonly AdminOptions adminOptions;


        public DataInitializer(IServiceProvider serviceProvider,
            IOptions<RolesOptions> rolesOptions,
            IOptions<AdminOptions> adminOptions,
            ITaskFolderRepository taskFolderRepository)
        {
            this.serviceProvider = serviceProvider;
            this.rolesOptions = rolesOptions.Value;
            this.adminOptions = adminOptions.Value;
            this.taskFolderRepository = taskFolderRepository;
        }


        public async Task Initialize()
        {
            await CreateRoles();
            await CreateAdmin();
            await CreateSystemFolders();
            await LinkSystemFoldersWithAdmin();
        }

        private async Task CreateAdmin()
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var user = await userManager.FindByEmailAsync(adminOptions.Email);

            if (user is null)
            {
                var admin = new User
                {
                    FirstName = adminOptions.Name,
                    SecondName = adminOptions.Secondname,
                    Email = adminOptions.Email,
                    UserName = adminOptions.Email
                };

                var result = await userManager.CreateAsync(admin, adminOptions.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, rolesOptions.Roles.First(r => r.Name == "Admin").Name);
                }
            }
        }

        private async Task CreateRoles()
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

            foreach (var role in rolesOptions.Roles)
            {
                if (await roleManager.RoleExistsAsync(role.Name))
                {
                    return;
                }
                await roleManager.CreateAsync(role);
            }
        }

        private async Task CreateSystemFolders()
        {
            var folders = await taskFolderRepository.GetAllAsync(f => f.Type == FolderType.Important ||
                                                                      f.Type == FolderType.MyDay ||
                                                                      f.Type == FolderType.Planned ||
                                                                      f.Type == FolderType.Tasks);
            if (folders.Count == 0)
            {
                List<TaskFolder> taskFolders = new()
                {
                    new TaskFolder
                    {
                        Name = "Мой день",
                        Type = FolderType.MyDay,
                    },
                    new TaskFolder
                    {
                        Name = "Задачи",
                        Type = FolderType.Tasks,
                    },
                    new TaskFolder
                    {
                        Name = "Запланированные",
                        Type = FolderType.Planned,
                    },
                    new TaskFolder
                    {
                        Name = "Важные",
                        Type = FolderType.Important,
                    }
                };

                await taskFolderRepository.CreateRangeAsync(taskFolders);
            }
        }

        private async Task LinkSystemFoldersWithAdmin()
        {

        }
    }
}
