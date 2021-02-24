using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Services.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.Shared;
using TaskManager.Shared.Infos;

namespace TaskManager.Core.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IMapper mapper;
        private readonly ITokenService tokenService;
        private readonly UserManager<User> userManager;


        public IdentityService(UserManager<User> userManager,
            IMapper mapper, ITokenService tokenService)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.tokenService = tokenService;
        }


        public async Task<UserTokens> Authorize(UserAuthorizeInfo info)
        {
            var user = await userManager.FindByEmailAsync(info.Email);
            if (user is not null && await userManager.CheckPasswordAsync(user, info.Password))
            {
                var claims = GetClaims(user);
                return new UserTokens
                {
                    AccessToken = tokenService.GenerateJwtToken(claims.Claims),
                    RefreshToken = tokenService.GenerateRefreshToken()
                };
            }
            throw new Exception("Wrong email or password");
        }

        public async Task<Guid> Register(UserRegistrationInfo info)
        {
            var user = await userManager.FindByEmailAsync(info.Email);
            if (user is null)
            {
                var newUser = mapper.Map<User>(info);
                newUser.UserName = info.Email;
                var res = await userManager.CreateAsync(newUser, info.Password);
                return newUser.Id;
            }
            throw new Exception("Email is already registered");
        }



        private ClaimsIdentity GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("Firstname", user.Firstname),
                new Claim("Lastname", user.Lastname),
                new Claim(ClaimTypes.Email, user.Email)
            };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme, ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }

    public class UserTokens
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
