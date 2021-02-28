using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManager.Core.Extensions;
using TaskManager.Core.Mapping;
using TaskManager.Data;
using TaskManager.Data.DataInitializers;
using TaskManager.Data.DataInitializers.Options;
using TaskManager.Domain.Models;

namespace TaskManager.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AdminOptions>(Configuration.GetSection(nameof(AdminOptions)));
            services.Configure<RolesOptions>(Configuration.GetSection(nameof(RolesOptions)));

            services.AddCustomDbContext<DatabaseContext>(Configuration);
            services.AddCustomIdentity<User, Role>()
                .AddRoles<Role>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapper(config => config.AddProfile<MapProfile>());
            services.AddDataInitializers();
            services.AddServices();
            services.AddRepositories();
            services.AddControllers();
            services.AddJwtAuthentication(Configuration);
            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
