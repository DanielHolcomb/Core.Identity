using Core.Identity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Identity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly ILogger<IdentityController> _logger;
        private readonly JwtConfigOptions _jwtConfig;
        private readonly StoredUserOptions _storedUser;

        public IdentityController(ILogger<IdentityController> logger, IOptions<JwtConfigOptions> jwtConfig, IOptions<StoredUserOptions> storedUser)
        {
            _logger = logger;
            _jwtConfig = jwtConfig.Value;
            _storedUser = storedUser.Value;
        }

        [HttpPost]
        [Route("Token")]
        public IActionResult Token([FromBody] User user)
        {
            if (user.Username != _storedUser.Username && user.Password != _storedUser.Password)
                return Forbid();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("source", "identity") }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtConfig.Issuer,
                Audience = _jwtConfig.Audience,
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(tokenHandler.WriteToken(token));
        }
    }
}