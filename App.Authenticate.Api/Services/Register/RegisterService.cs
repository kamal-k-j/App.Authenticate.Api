using App.Authenticate.Api.Data;
using App.Authenticate.Api.Entities.Request;

namespace App.Authenticate.Api.Services.Register
{
    public class RegisterService : IRegisterService
    {
        private readonly IMapUserRegister _mapUserRegister;
        private readonly IStoreUser _storeUser;

        public RegisterService(
            IMapUserRegister mapUserRegister,
            IStoreUser storeUser)
        {
            _mapUserRegister = mapUserRegister;
            _storeUser = storeUser;
        }

        public void Register(UserRegister userRegister)
        {
            var user = _mapUserRegister.Map(userRegister);
            _storeUser.Store(user);
        }
    }
}