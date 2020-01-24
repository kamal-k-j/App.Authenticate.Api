using App.Authenticate.Entities;

namespace App.Authenticate.Data.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public static explicit operator User(UserDto userDto)
        {
            return new User
            {
                Id = userDto.Id,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Token = string.Empty
            };
        }
    }
}
