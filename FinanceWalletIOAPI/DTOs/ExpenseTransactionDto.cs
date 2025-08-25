using FinanceWalletIOAPI.DTOs.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceWalletIOAPI.DTOs
{
    public sealed class OutTransactListDto : IApiResult
    {
        [Required] public Guid Id { get; set; }
        public string ExpenseName { get; set; } = null!;
        [Required] public decimal Amount { get; set; }
        [Required] public DateTime DeductDate { get; set; }  // When income was received
    }

    public sealed class OutTransactDetailsDto : IApiResult
    {
        public string ExpenseType { get; set; } = null!;
        public string ExpenseName { get; set; } = null!;
        [Required, Column(TypeName = "decimal(18,2)")] public decimal Amount { get; set; }
        [Required] public DateTime DeductDate { get; set; }  // When income was received
        [StringLength(500)] public string? Notes { get; set; }  // Optional note
        [Required] public bool IsAutoAdded { get; set; }  // Flag for recurring auto-entry
        [Required] public DateTime CreatedAt { get; set; }
    }

    public sealed class CreateOutTransactDto
    {
        [Required] public Guid ExpenseSourceId { get; set; }
        [Required] public decimal Amount { get; set; }
        [Required] public DateTime DeductDate { get; set; }  // When income was received
        [StringLength(500)] public string? Notes { get; set; }  // Optional note
    }

    public sealed class UpdateOutTransactDto
    {
        [Required] public Guid Id { get; set; }
        [Required] public Guid ExpenseSourceId { get; set; }
        [Required] public decimal Amount { get; set; }
        [Required] public DateTime DeductDate { get; set; }  // When income was received
        [StringLength(500)] public string? Notes { get; set; }  // Optional note
    }
}
