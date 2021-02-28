using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Auth;
using TaskManager.Core.Auth.Interfaces;
using TaskManager.Core.DataInitializers;
using TaskManager.Core.DataInitializers.Interfaces;
using TaskManager.Core.Services;
using TaskManager.Core.Services.Interfaces;
using TaskManager.Data.Repositories;
using TaskManager.Data.Repositories.Interfaces;

namespace TaskManager.Core.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static async Task<IHost> InitializeDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            await scope.ServiceProvider.GetRequiredService<IDataInitializer>().Initialize();

            return host;
        }

        public static IServiceCollection AddDataInitializers(this IServiceCollection services)
        {
            services.AddScoped<IDataInitializer, DataInitializer>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IToDoTaskRepository, ToDoTaskRepository>();
            services.AddScoped<ITaskFolderRepository, TaskFolderRepository>();
            services.AddScoped<ISubTaskRepository, SubTaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITodoTaskService, ToDoTaskService>();
            services.AddScoped<ITaskFolderService, TaskFolderService>();
            services.AddScoped<ISubTaskService, SubTaskService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();

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

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["BearerToken:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["BearerToken:Audience"],
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["BearerToken:Key"])),
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }
    }
}
