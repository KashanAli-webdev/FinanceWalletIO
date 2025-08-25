using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceWalletIOAPI.Models
{
    public sealed class ExpenseTransaction
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid ExpenseSourceId { get; set; }

        public string UserId { get; set; } = null!;

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime DeductDate { get; set; }  // When income was received

        [StringLength(500)]
        public string? Notes { get; set; }  // Optional note

        [Required]
        public bool IsAutoAdded { get; set; }  // Flag/Identify for recurring auto-entry

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
