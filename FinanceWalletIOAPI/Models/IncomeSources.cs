using FinanceWalletIOAPI.DTOs.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinanceWalletIOAPI.Models
{
    public class IncomeSources
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string UserId { get; set; } = null!;

        [Required]
        public IncomeStreams IncomeType { get; set; }

        [StringLength(30)]
        public string Name { get; set; } = null!;  // Custom label for the source (e.g., 'Upwork')

        [Required]
        public bool IsRecurring { get; set; }  // Indicates if it's a repeating income

        public IncomeInterval? RecurringInterval { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}