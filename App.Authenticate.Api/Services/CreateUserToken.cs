using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace App.Authenticate.Api.Services
{
    public class CreateUserToken : ICreateUserToken
    {
        public SecurityToken Create(JwtSecurityTokenHandler tokenHandler, int userId)
        {
            var key = Encoding.ASCII.GetBytes(Guid.NewGuid().ToString()); //TODO secret signing id
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };
            return tokenHandler.CreateToken(tokenDescriptor);
        }
    }
}