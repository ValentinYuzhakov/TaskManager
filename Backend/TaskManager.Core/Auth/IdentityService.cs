using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Auth.Interfaces;
using TaskManager.Core.Services.Interfaces;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.Shared;
using TaskManager.Shared.Infos;
using TaskManager.Shared.ViewModels;

namespace TaskManager.Core.Auth
{
    public class IdentityService : IIdentityService
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private readonly ITokenService tokenService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly HttpContext httpContext;
        private readonly IEmailService emailService;


        public IdentityService(UserManager<User> userManager,
            IMapper mapper, ITokenService tokenService,
            SignInManager<User> signInManager,
            IUserRepository userRepository,
            IHttpContextAccessor contextAccessor,
            IEmailService emailService)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.tokenService = tokenService;
            this.signInManager = signInManager;
            this.userRepository = userRepository;
            httpContext = contextAccessor.HttpContext;
            this.emailService = emailService;
        }


        public async Task<User> Register(UserRegistrationInfo info)
        {
            var user = await userManager.FindByEmailAsync(info.Email);
            if (user is null)
            {
                var newUser = mapper.Map<User>(info);
                var result = await userManager.CreateAsync(newUser, info.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, "User");
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(newUser);
                    await emailService.SendConfirmationEmailAsync(token);
                    return newUser;
                }

                var builder = new StringBuilder();

                foreach (var error in result.Errors)
                {
                    builder.AppendLine(error.Description);
                }

                throw new Exception(builder.ToString());
            }
            throw new Exception("Email is already registered");
        }

        public async Task<UserAuthorizeView> Authorize(UserAuthorizeInfo info)
        {
            var user = await userManager.FindByEmailAsync(info.Email);
            if (user is not null && (await signInManager.CheckPasswordSignInAsync(user, info.Password, false)).Succeeded)
            {
                var refreshToken = new RefreshToken
                {
                    Token = tokenService.GenerateRefreshToken(),
                    UserId = user.Id,
                    User = user,
                    ExpireDate = DateTime.Now.AddDays(1)
                };
                user.RefreshTokens.Add(refreshToken);
                await userRepository.UpdateAsync(user);
                return new UserAuthorizeView
                {
                    AccessToken = await tokenService.GenerateJwtToken(user),
                    RefreshToken = refreshToken.Token
                };
            }
            throw new Exception("Wrong email or password");
        }

        public async Task<RefreshResult> RefreshToken()
        {
            var jwtToken = await httpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
            var refreshToken = httpContext.Request.Headers["refresh-token"].ToString();

            return await tokenService.RefreshJwtToken(jwtToken, refreshToken);
        }

        public async Task RevokeRefreshToken()
        {
            var refreshToken = httpContext.Request.Headers["refresh-token"].ToString();
            var user = await userManager.GetUserAsync(httpContext.User);

            tokenService.RevokeRefreshToken(user, refreshToken);
            await userRepository.UpdateAsync(user);
        }

        public async Task ConfirmEmailAsync(Guid userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            await userManager.ConfirmEmailAsync(user, token);
        }
    }
}
