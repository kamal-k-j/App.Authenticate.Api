using App.Authenticate.Api.Entities.Request;
using App.Authenticate.Api.Entities.Response;

namespace App.Authenticate.Api.Services.Register
{
    public class MapUserRegister : IMapUserRegister
    {
        private readonly IPasswordManager _passwordManager;

        public MapUserRegister(IPasswordManager passwordManager)
        {
            _passwordManager = passwordManager;
        }

        public User Map(UserRegister userRegister)
        {
            var generatedPassword = _passwordManager.Generate(userRegister.Password);
            return new User
            {
                Id = 0,
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName,
                Email = userRegister.Email,
                PasswordHash = generatedPassword.PasswordHash,
                HashSalt = generatedPassword.PasswordSalt,
                Token = string.Empty,
                PhoneNumberCountry = userRegister.PhoneNumberCountry,
                PhoneNumber = userRegister.PhoneNumber,
                DateOfBirth = userRegister.DateOfBirth
            };
        }
    }
}