using App.Authenticate.Api.Entities.Request;
using App.Authenticate.Api.Entities.Response;

namespace App.Authenticate.Api.Services.Register
{
    public class MapUserRegister : IMapUserRegister
    {
        public User Map(UserRegister userRegister)
        {
            return new User
            {
                Id = 0,
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName,
                Email = userRegister.Email,
                PasswordHash = userRegister.PasswordHash,
                HashSalt = userRegister.HashSalt,
                Token = string.Empty,
                PhoneNumberCountry = userRegister.PhoneNumberCountry,
                PhoneNumber = userRegister.PhoneNumber,
                DateOfBirth = userRegister.DateOfBirth
            };
        }
    }
}