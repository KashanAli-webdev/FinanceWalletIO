using System.ComponentModel.DataAnnotations;

namespace FinanceWalletIOAPI.DTOs
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
    public class RegisterDto
    {
        public string Name { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
