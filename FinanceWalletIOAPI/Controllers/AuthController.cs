using FinanceWalletIOAPI.DTOs;
using FinanceWalletIOAPI.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace FinanceWalletIOAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authServices)
        {
            _authRepo = authServices;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var result = await _authRepo.RegisterAsync(dto);
            if (result.Status == false)
                return Conflict(result);

            return Ok(result);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _authRepo.LoginAsync(dto);
            if (token == null)
                return Unauthorized(token);

            return Ok(token);
        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _authRepo.LogoutAsync();
            return Ok(result);
        }
    }
}
