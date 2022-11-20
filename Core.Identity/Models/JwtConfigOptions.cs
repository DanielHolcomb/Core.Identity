namespace Core.Identity.Models
{
    public class JwtConfigOptions
    {
        public const string Name = "Jwt";

        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
