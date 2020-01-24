using App.Authenticate.Api.Entities;

namespace App.Authenticate.Api.Services
{
    public interface IAuthenticateService
    {
        User Authenticate(string email, string password);
    }
}
