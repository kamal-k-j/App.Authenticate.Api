using App.Authenticate.Entities;

namespace App.Authenticate.Services
{
    public interface IAuthenticateService
    {
        User Authenticate(string email, string password);
    }
}
