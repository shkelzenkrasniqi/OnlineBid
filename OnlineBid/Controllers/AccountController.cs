using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace OnlineBid.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            // Check if model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // Return validation errors automatically

            var user = await _accountService.Login(loginDto);
            if (user != null)
                return Ok(user);

            return BadRequest(new { message = "Invalid email or password" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            // Check if model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // Return validation errors automatically

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
