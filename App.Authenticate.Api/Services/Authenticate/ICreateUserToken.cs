using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace App.Authenticate.Api.Services.Authenticate
{
    public interface ICreateUserToken
    {
        SecurityToken Create(JwtSecurityTokenHandler tokenHandler, int userId);
    }
}
