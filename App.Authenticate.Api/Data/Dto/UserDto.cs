using App.Authenticate.Api.Entities.Response;
using App.Data.Conventions;
using System;

namespace App.Authenticate.Api.Data.Dto
{
    [Entity]
    public class UserDto
    {
        public virtual int Id { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string Email { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual string HashSalt { get; set; }

        public virtual DateTime DateOfBirth { get; set; }

        public virtual string PhoneNumberCountry { get; set; }

        public virtual string PhoneNumber { get; set; }

        public static explicit operator User(UserDto userDto)
        {
            return new User
            {
                Id = userDto.Id,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Token = string.Empty,
                PasswordHash = userDto.PasswordHash,
                HashSalt = userDto.HashSalt,
                DateOfBirth = userDto.DateOfBirth,
                PhoneNumber = userDto.PhoneNumber,
                PhoneNumberCountry = userDto.PhoneNumberCountry
            };
        }

        public static explicit operator UserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                HashSalt = user.HashSalt,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberCountry = user.PhoneNumberCountry
            };
        }
    }
}
