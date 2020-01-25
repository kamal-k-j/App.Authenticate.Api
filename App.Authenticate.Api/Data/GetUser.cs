using System.Linq;
using App.Authenticate.Api.Data.Dto;
using App.Authenticate.Api.Entities.Response;
using App.Authenticate.Api.Services;
using App.Data.UoW;

namespace App.Authenticate.Api.Data
{
    public class GetUser : IGetUser
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordManager _passwordManager;

        public GetUser(
            IUnitOfWork unitOfWork,
            IPasswordManager passwordManager)
        {
            _unitOfWork = unitOfWork;
            _passwordManager = passwordManager;
        }

        public User Get(string email, string password)
        {
            var userDto = _unitOfWork.Session
                .QueryOver<UserDto>()
                .Where(u => u.Email == email)
                .SingleOrDefault();
            return userDto.Email == email && _passwordManager.Verify(userDto.PasswordHash, userDto.HashSalt, password)
                    ? (User)userDto
                    : null;
        }
    }
}