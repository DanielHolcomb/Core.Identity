namespace Core.Identity.Models
{
    public class JwtConfig
    {
        public const string Name = "Jwt";

        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
