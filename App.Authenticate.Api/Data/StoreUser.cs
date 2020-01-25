using App.Authenticate.Api.Data.Dto;
using App.Authenticate.Api.Entities.Response;
using App.Data.UoW;

namespace App.Authenticate.Api.Data
{
    public class StoreUser : IStoreUser
    {
        private readonly IUnitOfWork _unitOfWork;

        public StoreUser(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Store(User user)
        {
            _unitOfWork.Session.Save((UserDto)user);
        }
    }
}