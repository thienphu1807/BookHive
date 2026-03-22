using BookHiveApi.Models.Dtos;
using BookHiveApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookHiveApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.Register(dto);
            return result.Succeeded ? Ok(new { message = "Register Successfull" }) : BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _authService.Login(dto);
            if (user == null) return Unauthorized(new { message = "Wrong Email or Password" });

            return Ok(user);
        }

        [HttpPost("updatepassword")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordDto dto)
        {
            var result = await _authService.UpdatePassword(dto);
            if(result == null)
            {
                return BadRequest("Something wrong");
            }
            return Ok("The Password has been updated");
        }
        [HttpPost("updateRole")]
        public async Task<IActionResult> UpdateRole(string userEmail)
        {
            var result = await _authService.UpdateRole(userEmail);
            if (result == null)
            {
                return BadRequest("Something wrong");
            }
            return Ok("The role has been updated");
        }
        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            var userName = User.Identity?.Name;
            var email = User.FindFirstValue(ClaimTypes.Email);
            var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
            return Ok (new { userName, email, roles });
        }
    }
}
