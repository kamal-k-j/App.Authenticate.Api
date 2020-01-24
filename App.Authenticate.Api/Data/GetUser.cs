using App.Authenticate.Api.Data.Dto;
using App.Authenticate.Api.Entities;

namespace App.Authenticate.Api.Data
{
    public class GetUser : IGetUser
    {
        public User Get(string email, string password)
        {
            //TODO database stuff
            var userDto = new UserDto
            {
                Id = 1,
                Email = "kamal.jassal@outlook.com",
                FirstName = "Kamal",
                LastName = "Jassal",
                Password = "Password123."
            };
            return userDto.Email == email && userDto.Password == password
                    ? (User)userDto
                    : null;
        }
    }
}