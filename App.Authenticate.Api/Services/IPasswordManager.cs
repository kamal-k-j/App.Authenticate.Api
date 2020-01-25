namespace App.Authenticate.Api.Services
{
    public interface IPasswordManager
    {
        GeneratedPassword Generate(string password);

        bool Verify(string hashedPassword, string salt, string passwordToVerify);
    }
}