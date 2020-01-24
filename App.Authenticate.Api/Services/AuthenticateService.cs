using App.Authenticate.Api.Data;
using App.Authenticate.Api.Entities.Response;
using System.IdentityModel.Tokens.Jwt;

namespace App.Authenticate.Api.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IGetUser _getUser;
        private readonly ICreateUserToken _createUserToken;

        public AuthenticateService(
            IGetUser getUser,
            ICreateUserToken createUserToken)
        {
            _getUser = getUser;
            _createUserToken = createUserToken;
        }

        public User Authenticate(string email, string password)
        {
            var user = _getUser.Get(email, password);

            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = _createUserToken.Create(tokenHandler, user.Id);
            user.Token = tokenHandler.WriteToken(token);
            return user;
        }
    }
}