using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Data.Services;
using TaskManager.Data.Services.Interfaces;

namespace TaskManager.Core.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITodoTaskService, ToDoTaskService>();

            return services;
        }
    }
}
