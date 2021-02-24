using Microsoft.Extensions.DependencyInjection;
using TaskManager.Core.Services;
using TaskManager.Core.Services.Interfaces;

namespace TaskManager.Core.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITodoTaskService, ToDoTaskService>();
            services.AddScoped<ITaskFolderService, TaskFolderService>();
            services.AddScoped<ISubTaskService, SubTaskService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
