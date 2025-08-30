using Hotel.Application.Auth;
using Hotel.Application.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var result = await _authService.RegisterAsync(dto);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { token = result.Data } );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {

            var token = await _authService.LoginAsync(dto);

            if (token is null || token.StartsWith("Invalid") || token.Contains("no assigned role"))
            {
                return Unauthorized(new { message = token });
            }

            return Ok(new { token } );
        }
    }

}
