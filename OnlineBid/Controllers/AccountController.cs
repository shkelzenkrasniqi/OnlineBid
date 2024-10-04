using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace OnlineBid.Controllers
{
    public class AccountController(IAccountService _accountService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var user = await _accountService.Login(loginDto);
            if (user != null)
                return Ok(user);

            return BadRequest(new { message = "Invalid email or password" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            try
            {
                var user = await _accountService.Register(registerDto);
                return CreatedAtAction(nameof(Login), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                if (ex.Message == "User already exists" || ex.Message.Contains("Password"))
                {
                    return BadRequest(new { message = ex.Message });
                }

                return StatusCode(500, ex.Message);
            }
        }
    }
}