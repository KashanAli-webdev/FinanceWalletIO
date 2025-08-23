using FinanceWalletIOAPI.DTOs.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceWalletIOAPI.DTOs
{
    public sealed class InTransactListDto : IApiResult
    {
        [Required] public Guid Id { get; set; }
        public string IncomeName { get; set; } = null!;
        [Required] public decimal Amount { get; set; }
        [Required] public DateTime ReceivedDate { get; set; }  // When income was received
    }

    public sealed class InTransactDetailsDto : IApiResult
    {
        public string IncomeType { get; set; } = null!;
        public string IncomeName { get; set; } = null!;
        [Required, Column(TypeName = "decimal(18,2)")] public decimal Amount { get; set; }
        [Required] public DateTime ReceivedDate { get; set; }  // When income was received
        [StringLength(500)] public string? Notes { get; set; }  // Optional note
        [Required] public bool IsAutoAdded { get; set; }  // Flag for recurring auto-entry
        [Required] public DateTime CreatedAt { get; set; }
    }

    public sealed class CreateInTransactDto
    {
        [Required] public Guid IncomeSourceId { get; set; }
        [Required] public decimal Amount { get; set; }
        [Required] public DateTime ReceivedDate { get; set; }  // When income was received
        [StringLength(500)] public string? Notes { get; set; }  // Optional note
    }

    public sealed class UpdateInTransactDto
    {
        [Required] public Guid Id { get; set; }
        [Required] public Guid IncomeSourceId { get; set; }
        [Required] public decimal Amount { get; set; }
        [Required] public DateTime ReceivedDate { get; set; }  // When income was received
        [StringLength(500)] public string? Notes { get; set; }  // Optional note
    }
}
