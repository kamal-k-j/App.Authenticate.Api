using App.Authenticate.Api.Entities.Request;

namespace App.Authenticate.Api.Services
{
    public interface IRegisterService
    {
        void Register(UserRegister userRegister);
    }
}
