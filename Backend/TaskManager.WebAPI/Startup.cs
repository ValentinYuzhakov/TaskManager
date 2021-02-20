using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManager.Core.Extensions;
using TaskManager.Core.Mapping;
using TaskManager.Data;
using TaskManager.Data.DataInitializers;
using TaskManager.Data.DataInitializers.Options;
using TaskManager.Data.Extensions;
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
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TaskManagerLocalDB"),
                b => b.MigrationsAssembly("TaskManager.Data")));



            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<DatabaseContext>();


            services.Configure<AdminOptions>(Configuration.GetSection(nameof(AdminOptions)));
            services.Configure<RolesOptions>(Configuration.GetSection(nameof(RolesOptions)));
            services.AddAutoMapper(config => config.AddProfile<MapProfile>());
            services.AddDataInitializers();
            services.AddServices();
            services.AddRepositories();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
