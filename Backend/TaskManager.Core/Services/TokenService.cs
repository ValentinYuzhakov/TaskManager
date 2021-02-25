using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Services.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Core.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<User> userManager;


        public TokenService(IConfiguration configuration,
            UserManager<User> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
        }


        public async Task<string> GenerateJwtToken(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["BearerToken:Key"]));

            var token = new JwtSecurityToken(
                   issuer: configuration["BearerToken:Issuer"],
                   audience: configuration["BearerToken:Audience"],
                   claims: (await GetClaims(user)).Claims,
                   expires: DateTime.Now.AddMinutes(double.Parse(configuration["BearerToken:LifeTime"])),
                   signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public Task<RefreshResult> Refresh(string jwtToken, string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task RevokeRefreshToken(User user, string refreshToken)
        {
            throw new NotImplementedException();
        }

        private async Task<ClaimsIdentity> GetClaims(User user)
        {
            var userRole = (await userManager.GetRolesAsync(user)).First();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Firstname),
                new Claim("Lastname", user.Lastname),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, userRole)
            };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme, ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
