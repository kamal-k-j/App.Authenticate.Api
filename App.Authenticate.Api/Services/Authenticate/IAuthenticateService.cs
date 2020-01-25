using App.Authenticate.Api.Entities.Response;

namespace App.Authenticate.Api.Services.Authenticate
{
    public interface IAuthenticateService
    {
        User Authenticate(string email, string password);
    }
}
