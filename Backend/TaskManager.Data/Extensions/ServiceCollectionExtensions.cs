using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TaskManager.Data.Repositories;
using TaskManager.Data.Repositories.Interfaces;

namespace TaskManager.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IToDoTaskRepository, ToDoTaskRepository>();
            services.AddScoped<ITaskFolderRepository, TaskFolderRepository>();
            services.AddScoped<ISubTaskRepository, SubTaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection AddCustomDbContext<TContext>(this IServiceCollection services, IConfiguration configuration)
            where TContext : DbContext
        {
            services.AddDbContext<TContext>(options => options.UseSqlServer(configuration.GetConnectionString("TaskManagerLocalDB"),
                 b => b.MigrationsAssembly("TaskManager.Data")));

            return services;
        }

        public static IdentityBuilder AddCustomIdentity<TUser, TRole>(this IServiceCollection services)
            where TUser : IdentityUser<Guid>
            where TRole : IdentityRole<Guid>
        {
            return services.AddIdentity<TUser, TRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
                options.User.RequireUniqueEmail = true;
            });
        }
    }
}
