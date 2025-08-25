using FinanceWalletIOAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace FinanceWalletIOAPI.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<IncomeSources> IncomeSources { get; set; }
        public DbSet<IncomeTransaction> IncomeTransactions { get; set; }
        public DbSet<ExpenseSources> ExpenseSources { get; set; }
        public DbSet<ExpenseTransaction> ExpenseTransactions { get; set; }
    }
}
