using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtWebAPITemplate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { message = "Pong" });
        }

        [HttpGet]
        [Route("pingtwo")]
        public IActionResult GetTwo()
        {
            return Ok(new { message = "Pong Two" });
        }

        [Authorize]
        [HttpGet]
        [Route("pingsecure")]
        public IActionResult GetSecure()
        {
            return Ok(new { message = "Pong Secure" });
        }
    }
}
