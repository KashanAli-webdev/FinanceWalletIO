using Microsoft.AspNetCore.Identity;

namespace FinanceWalletIOAPI.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; } = null!;
    }
}
