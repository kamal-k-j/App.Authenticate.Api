namespace App.Authenticate.Api.Services
{
    public class GeneratedPassword
    {
        public GeneratedPassword(
            string passwordHash,
            string hashSalt)
        {
            PasswordHash = passwordHash;
            HashSalt = hashSalt;
        }

        public string PasswordHash { get; }

        public string HashSalt { get; }
    }
}
