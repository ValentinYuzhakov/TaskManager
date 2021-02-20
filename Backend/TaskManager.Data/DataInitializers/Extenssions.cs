using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using TaskManager.Data.DataInitializers.Interfaces;

namespace TaskManager.Data.DataInitializers
{
    public static class Extenssions
    {
        public static async Task<IHost> InitializeDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                await scope.ServiceProvider.GetRequiredService<IDataInitializer>().Initialize();
            }

            return host;
        }

        public static IServiceCollection AddDataInitializers(this IServiceCollection services)
        {
            services.AddScoped<IDataInitializer, DataInitializer>();

            return services;
        }
    }
}
