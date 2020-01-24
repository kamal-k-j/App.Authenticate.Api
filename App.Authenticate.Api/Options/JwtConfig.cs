namespace App.Authenticate.Api.Options
{
    public class JwtConfig
    {
        public string SecretKey { get; set; }

        public byte ExpireDays { get; set; }
    }
}
