using FinanceWalletIOAPI.DTOs;
using FinanceWalletIOAPI.IRepositories;
using FinanceWalletIOAPI.IServices;
using Microsoft.AspNetCore.Mvc;

namespace FinanceWalletIOAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly IResponseService _resServ;
        public AuthController(IAuthRepository authServices, IResponseService resServ)
        {
            _authRepo = authServices;
            _resServ = resServ;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var res = await _authRepo.RegisterAsync(dto);
            if (!res.Status)  // Check if res is ResponseDto than assign a new variable(dto) to the res.
                return _resServ.HttpRes(this, res);

            return Ok(res);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var res = await _authRepo.LoginAsync(dto);
            if (!res.Status)
                return _resServ.HttpRes(this, res);

            return Ok(res);
        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var res = await _authRepo.LogoutAsync();
            return Ok(res);
        }
    }
}
