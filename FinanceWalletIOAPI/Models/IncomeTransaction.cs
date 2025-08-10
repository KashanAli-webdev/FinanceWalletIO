using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceWalletIOAPI.Models
{
    public class IncomeTransaction
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid IncomeSourceId { get; set; }

        public string UserId { get; set; } = null!;

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime ReceivedDate { get; set; }  // When income was received

        [StringLength(500)]
        public string? Notes { get; set; }  // Optional note

        [Required]
        public bool IsRecurringInstance { get; set; }  // Flag for recurring auto-entry

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
