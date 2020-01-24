using App.Authenticate.Entities;

namespace App.Authenticate.Data
{
    public interface IGetUser
    {
        User Get(string email, string password);
    }
}
