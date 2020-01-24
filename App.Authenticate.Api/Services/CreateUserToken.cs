using App.Authenticate.Api.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.Authenticate.Api.Services
{
    public class CreateUserToken : ICreateUserToken
    {
        private readonly IOptions<JwtConfig> _jwtConfigOptions;

        public CreateUserToken(IOptions<JwtConfig> jwtConfigOptions)
        {
            _jwtConfigOptions = jwtConfigOptions;
        }

        public SecurityToken Create(JwtSecurityTokenHandler tokenHandler, int userId)
        {
            var key = Encoding.ASCII.GetBytes(_jwtConfigOptions.Value.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(_jwtConfigOptions.Value.ExpireDays),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };
            return tokenHandler.CreateToken(tokenDescriptor);
        }
    }
}