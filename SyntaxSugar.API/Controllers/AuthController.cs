using Microsoft.AspNetCore.Mvc;
using SyntaxSugar.Application.DTOs;
using SyntaxSugar.Application.Interface.Auth;

namespace SyntaxSugar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterUserDTO registerUserDTO)
        {
            if (registerUserDTO == null)
            {
                return BadRequest("Invalid user data.");
            }
            var result = await _authService.RegisterUserAsync(registerUserDTO);
            if (result)
            {
                return Ok("User registered successfully.");
            }
            else
            {
                return BadRequest("User registration failed. User may already exist.");
            }
        }
        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser(LoginUserDTO loginUserDTO)
        {
            if (loginUserDTO == null)
            {
                return BadRequest("Invalid login data.");
            }
            var result = await _authService.LoginUserAsync(loginUserDTO);
            if (result != null && result.Succeeded)
            {
                return Ok(result);
            }
            else
            {
                return Unauthorized(new { message = result.Error });
            }
        }

        [HttpPost("LogoutUser")]
        public async Task<IActionResult> LogoutUser(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required for logout.");
            }
            await _authService.LogoutUserAsync(token);
            return Ok("User logged out successfully.");
        }

    }
}
