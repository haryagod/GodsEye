
using AuthService.Helper;
using AuthService.Models;
using Microsoft.AspNetCore.Mvc;
using AuthService.Models;
using AuthService.Services;
namespace AuthService.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("validate")]
        public async Task<IActionResult> validate()
        {
            var response = await _userService.Validate(HttpContext);

            if (response == null)
                return Unauthorized(new { message = "Invalid or missing Token" });

            return Ok(response);
        }
    }

}