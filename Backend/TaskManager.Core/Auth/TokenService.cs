using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            var email = result.ClaimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var user = await userManager.Users.Include(u => u.RefreshTokens).FirstOrDefaultAsync(u => u.Email == email);
            if (result.IsValid && user is not null && ValidateRefreshToken(user, refreshToken))
            {
                return new RefreshResult
                {
                    AccessToken = await GenerateJwtToken(user),
                    RefreshToken = GenerateRefreshToken()
                };
            }
            throw new Exception();
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["BearerToken:Key"])),
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                }, out var jwtToken);
            return new ValidateResult { ClaimsPrincipal = principal, Token = jwtToken as JwtSecurityToken, IsValid = true };
        }

        private bool ValidateRefreshToken(User user, string refreshToken)
        {
            var token = user.RefreshTokens.FirstOrDefault(t => t.Token.Contains(refreshToken));
            if (token is not null && token.IsActive)
            {
                return true;
            }
            return false;
        }
    }
}
