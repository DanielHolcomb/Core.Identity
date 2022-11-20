using Microsoft.AspNetCore.Mvc;

namespace Core.Identity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly ILogger<IdentityController> _logger;

        public IdentityController(ILogger<IdentityController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Token")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}