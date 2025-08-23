using FinanceWalletIOAPI.DTOs.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinanceWalletIOAPI.Models
{
    public sealed class ExpenseSource
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string UserId { get; set; } = null!;

        [Required]
        public ExpenseStreams ExpenseType { get; set; }

        [StringLength(30)]
        public string Name { get; set; } = null!;  // Custom label for the source (e.g., 'Upwork')

        [Required]
        public bool AutoRepeat { get; set; }  // Indicates if it's a repeating income

        [EnumDataType(typeof(IncomeInterval))]
        public IncomeInterval RepeatInterval { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
