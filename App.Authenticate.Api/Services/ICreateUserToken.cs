using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace App.Authenticate.Api.Services
{
    public interface ICreateUserToken
    {
        SecurityToken Create(JwtSecurityTokenHandler tokenHandler, int userId);
    }
}
