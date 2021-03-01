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
using TaskManager.Core.Auth.Interfaces;
using TaskManager.Core.Services.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Core.Auth
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
                   notBefore: DateTime.Now,
                   claims: await GetClaims(user),
                   expires: DateTime.Now.AddMinutes(double.Parse(configuration["BearerToken:LifeTime"])),
                   signingCredentials: new SigningCredentials(secretKey, configuration["BearerToken:Algorithm"]));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<RefreshResult> RefreshJwtToken(string jwtToken, string refreshToken)
        {
            var result = ValidateJwtToken(jwtToken);

            if (result.IsValid)
            {



            }




            return new RefreshResult
            {

            };
        }

        public void RevokeRefreshToken(User user, string refreshToken)
        {
            var userRefreshToken = user.RefreshTokens.FirstOrDefault();
            if (userRefreshToken is null || !userRefreshToken.IsActive)
            {
                throw new Exception("Invalid Token");
            }

            userRefreshToken.RevokeDate = DateTime.Now;
        }

        private async Task<IEnumerable<Claim>> GetClaims(User user)
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
            //ClaimsIdentity claimsIdentity =
            //new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme, ClaimsIdentity.DefaultNameClaimType,
            //    ClaimsIdentity.DefaultRoleClaimType);
            return claims;
        }

        private ValidateResult ValidateJwtToken(string token)
        {
            var principal = new JwtSecurityTokenHandler()
                .ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["BearerToken:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["BearerToken:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["BearerToken:Key"])),
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                }, out var jwtToken);
            return new ValidateResult { ClaimsPrincipal = principal, Token = jwtToken as JwtSecurityToken, IsValid = true };
        }

        private void ValidateRefreshToken(string refreshToken)
        {

        }
    }
}
