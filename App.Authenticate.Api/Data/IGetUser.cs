using App.Authenticate.Api.Entities;

namespace App.Authenticate.Api.Data
{
    public interface IGetUser
    {
        User Get(string email, string password);
    }
}
