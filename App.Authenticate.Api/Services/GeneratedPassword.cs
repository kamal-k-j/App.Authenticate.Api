namespace App.Authenticate.Api.Services
{
    public class GeneratedPassword
    {
        public GeneratedPassword(
            string passwordHash,
            string passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public string PasswordHash { get; }

        public string PasswordSalt { get; }
    }
}
