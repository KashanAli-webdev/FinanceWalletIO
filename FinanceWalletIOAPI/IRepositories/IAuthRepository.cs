using FinanceWalletIOAPI.DTOs;
using FinanceWalletIOAPI.DTOs.Base;
using FinanceWalletIOAPI.Models;

namespace FinanceWalletIOAPI.IRepositories
{
    public interface IAuthRepository
    {
        public Task<ResponseDto> RegisterAsync(RegisterDto dto);
        public Task<ResponseDto> LoginAsync(LoginDto dto);
        public string GenerateJWToken(User user);
        public Task<ResponseDto> LogoutAsync();
    }
}
