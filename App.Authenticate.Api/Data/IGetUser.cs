using App.Authenticate.Api.Entities.Response;

namespace App.Authenticate.Api.Data
{
    public interface IGetUser
    {
        User Get(string email, string password);
    }
}
