using App.Authenticate.Api.Entities.Request;
using App.Authenticate.Api.Entities.Response;

namespace App.Authenticate.Api.Services.Register
{
    public interface IMapUserRegister
    {
        User Map(UserRegister userRegister);
    }
}
