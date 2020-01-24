using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace App.Authenticate.Services
{
    public interface ICreateUserToken
    {
        SecurityToken Create(JwtSecurityTokenHandler tokenHandler, int userId);
    }
}
