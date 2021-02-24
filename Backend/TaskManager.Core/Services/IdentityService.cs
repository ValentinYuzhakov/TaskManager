using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        private readonly IConfiguration configuration;
        private readonly UserManager<User> userManager;


        public IdentityService(UserManager<User> userManager,
            IConfiguration configuration, IMapper mapper)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.mapper = mapper;
        }


        public async Task<string> Authorize(UserAuthorizeInfo info)
        {
            var user = await userManager.FindByEmailAsync(info.Email);
            if (user is not null && await userManager.CheckPasswordAsync(user, info.Password))
            {
                var claims = GetClaims(user);
                return GetJwtToken(claims);
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

        private string GetJwtToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                    issuer: configuration["BearerToken:Issuer"],
                    audience: configuration["BearerToken:Audience"],
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(configuration.GetValue<double>("BearerToken:LifeTime"))),
                    signingCredentials: new SigningCredentials(GetKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(token);
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

        private SymmetricSecurityKey GetKey()
        {
            var key = configuration["BearerToken:Key"];
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }
    }
}
