using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Movie.Services.Enums;
using Movie.Web.API.Services.Abstractions;
using Movie.Web.API.Services.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Movie.Web.API.Services.Implementations
{
    public class JwtService : IJwtService
    {
        private readonly string _secret;
        private readonly int _expDateInMinutes;

        public JwtService(IOptions<JwtConfiguration> options)
        {
            _secret = options.Value.Secret;
            _expDateInMinutes = options.Value.ExpirationInMinutes;
        }

        public JwtToken GenerateSecurityToken(string userName, Guid userId, Roles role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.NameIdentifier ,userId.ToString()),
                    new Claim(ClaimTypes.Role, role.ToString()),
                }),

                Expires = DateTime.UtcNow.AddMinutes(_expDateInMinutes),
                Audience = "localhost",
                Issuer = "localhost",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new JwtToken()
            {
                Token = tokenHandler.WriteToken(token)
            };

        }
    }
}
