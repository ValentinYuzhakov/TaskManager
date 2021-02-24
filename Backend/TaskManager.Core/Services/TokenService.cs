using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskManager.Core.Services.Interfaces;

namespace TaskManager.Core.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;


        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public string GenerateJwtToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["BearerToken:Key"]));

            var token = new JwtSecurityToken(
                   issuer: configuration["BearerToken:Issuer"],
                   audience: configuration["BearerToken:Audience"],
                   notBefore: DateTime.Now,
                   claims: claims,
                   expires: DateTime.Now.Add(TimeSpan.FromMinutes(double.Parse(configuration["BearerToken:LifeTime"]))),
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
    }
}
