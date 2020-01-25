using App.Authenticate.Api.Options;
using Microsoft.Extensions.Options;
using System;
using System.Security.Cryptography;

namespace App.Authenticate.Api.Services
{
    public class PasswordManager : IPasswordManager
    {
        private readonly IOptions<SecurityConfig> _securityConfigOptions;

        public PasswordManager(IOptions<SecurityConfig> securityConfigOptions)
        {
            _securityConfigOptions = securityConfigOptions;
        }

        public GeneratedPassword Generate(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[_securityConfigOptions.Value.SaltSize]);
            var hash = new Rfc2898DeriveBytes(password, salt, _securityConfigOptions.Value.HashIterations).GetBytes(_securityConfigOptions.Value.HashSize);
            return new GeneratedPassword(Convert.ToBase64String(hash), Convert.ToBase64String(salt));
        }

        public bool Verify(string hashedPassword, string salt, string passwordToVerify)
        {
            var hashBytes = Convert.FromBase64String(hashedPassword);
            var passwordHashBytes = new Rfc2898DeriveBytes(passwordToVerify, Convert.FromBase64String(salt), _securityConfigOptions.Value.HashIterations);
            var hash = passwordHashBytes.GetBytes(_securityConfigOptions.Value.HashSize);
            var result = true;
            for (var i = 0; i < _securityConfigOptions.Value.HashSize; i++)
            {
                if (hashBytes[i] == hash[i]) continue;
                result = false;
                break;
            }

            return result;
        }
    }
}
