using Microsoft.AspNetCore.Identity;

namespace FinanceWalletIOAPI.Models
{
    public sealed class User : IdentityUser
    {
        public string Name { get; set; } = null!;
    }
}
