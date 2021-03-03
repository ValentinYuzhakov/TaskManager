using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.DataInitializers.Interfaces;
using TaskManager.Core.DataInitializers.Options;
using TaskManager.Core.Services.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Core.DataInitializers
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IServiceProvider serviceProvider;
        private readonly RolesOptions rolesOptions;
        private readonly AdminOptions adminOptions;


        public DataInitializer(IServiceProvider serviceProvider,
            IOptions<RolesOptions> rolesOptions,
            IOptions<AdminOptions> adminOptions)
        {
            this.serviceProvider = serviceProvider;
            this.rolesOptions = rolesOptions.Value;
            this.adminOptions = adminOptions.Value;
        }


        public async Task Initialize()
        {
            await CreateRoles();
            await CreateAdmin();
        }

        private async Task CreateAdmin()
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var userService = serviceProvider.GetRequiredService<IUserService>();
            var user = await userManager.FindByEmailAsync(adminOptions.Email);

            if (user is null)
            {
                var admin = new User
                {
                    Firstname = adminOptions.Name,
                    Lastname = adminOptions.Secondname,
                    Email = adminOptions.Email,
                    UserName = adminOptions.Email
                };

                var result = await userManager.CreateAsync(admin, adminOptions.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, rolesOptions.Roles.First(r => r.Name == "Admin").Name);
                    await userService.InitializeSystemFolders(admin);
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
    }
}
