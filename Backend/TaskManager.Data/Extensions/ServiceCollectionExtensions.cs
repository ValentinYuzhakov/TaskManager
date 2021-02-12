using Microsoft.Extensions.DependencyInjection;
using TaskManager.Data.DataContext;
using TaskManager.Data.DataContext.Interfaces;
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

            return services;
        }

        public static IServiceCollection AddDataContext(this IServiceCollection services)
        {
            services.AddScoped(typeof(IDataContext<>), typeof(SqlContext<>));

            return services;
        }
    }
}
