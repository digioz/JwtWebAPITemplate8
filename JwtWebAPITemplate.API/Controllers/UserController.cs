using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JwtWebAPITemplate.API.Models;
using JwtWebAPITemplate.API.Services;

namespace JwtWebAPITemplate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        //[HttpGet]
        //[Authorize]
        //public async Task<IActionResult> Get()
        //{
        //    return Ok(await _userService.GetAll());
        //}

        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> Post([FromBody] User userObj)
        //{
        //    userObj.Id = 0;
        //    return Ok(await _userService.AddAndUpdateUser(userObj));
        //}

        //// PUT api/<CustomerController>/5
        //[HttpPut("{id}")]
        //[Authorize]
        //public async Task<IActionResult> Put(int id, [FromBody] User userObj)
        //{
        //    userObj.Id = id;
        //    return Ok(await _userService.AddAndUpdateUser(userObj));
        //}

        [HttpPost("login")]
        public async Task<IActionResult> login(AuthenticateRequest model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

    }
}
