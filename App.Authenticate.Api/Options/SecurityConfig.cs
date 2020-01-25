namespace App.Authenticate.Api.Options
{
    public class SecurityConfig
    {
        public int HashSize { get; set; }
        public int HashIterations { get; set; }
        public int SaltSize { get; set; }
    }
}
