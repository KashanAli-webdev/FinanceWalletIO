using FinanceWalletIOAPI.DTOs.Base;
using FinanceWalletIOAPI.DTOs.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinanceWalletIOAPI.DTOs
{
    public class IncomeListDto : IApiResult
    {
        public Guid Id { get; set; }
        public IncomeStreams IncomeType { get; set; }
        public string Name { get; set; } = null!;  // Custom label for the source (e.g., 'Upwork')
        public IncomeInterval? RecurringInterval { get; set; }
        public string? Notes { get; set; }
    }

    public class IncomeDetailsDto : IApiResult
    {
        public IncomeStreams IncomeType { get; set; }
        public string Name { get; set; } = null!;  // Custom label for the source (e.g., 'Upwork')
        public bool IsRecurring { get; set; }  // Indicates if it's a repeating income
        public IncomeInterval? RecurringInterval { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateIncomeDto
    {
        [Required] public IncomeStreams IncomeType { get; set; }
        [StringLength(30)] public string Name { get; set; } = null!;  // Custom label for the source (e.g., 'Upwork')
        [Required] public bool IsRecurring { get; set; }  // Indicates if it's a repeating income
        public IncomeInterval? RecurringInterval { get; set; }
        [StringLength(500)] public string? Notes { get; set; }
    }

    public class UpdateIncomeDto
    {
        public Guid Id { get; set; }
        public IncomeStreams IncomeType { get; set; }
        public string Name { get; set; } = null!;  // Custom label for the source (e.g., 'Upwork')
        public bool IsRecurring { get; set; }  // Indicates if it's a repeating income
        public IncomeInterval? RecurringInterval { get; set; }
        public string? Notes { get; set; }
    }
}
