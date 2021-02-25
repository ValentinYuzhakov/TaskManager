using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Services.Interfaces;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.Shared;
using TaskManager.Shared.Infos;
using TaskManager.Shared.ViewModels;

namespace TaskManager.Core.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private readonly ITokenService tokenService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;


        public IdentityService(UserManager<User> userManager,
            IMapper mapper, ITokenService tokenService,
            SignInManager<User> signInManager, IUserRepository userRepository)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.tokenService = tokenService;
            this.signInManager = signInManager;
            this.userRepository = userRepository;
        }


        public async Task<Guid> Register(UserRegistrationInfo info)
        {
            var user = await userManager.FindByEmailAsync(info.Email);
            if (user is null)
            {
                var newUser = mapper.Map<User>(info);
                var result = await userManager.CreateAsync(newUser, info.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, "User");
                    return newUser.Id;
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
                await userRepository.SaveChangesAsync();
                return new UserAuthorizeView
                {
                    AccessToken = await tokenService.GenerateJwtToken(user),
                    RefreshToken = refreshToken.Token
                };
            }
            throw new Exception("Wrong email or password");
        }
    }
}
