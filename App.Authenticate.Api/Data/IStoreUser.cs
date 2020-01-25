using App.Authenticate.Api.Entities.Response;

namespace App.Authenticate.Api.Data
{
    public interface IStoreUser
    {
        void Store(User user);
    }
}
