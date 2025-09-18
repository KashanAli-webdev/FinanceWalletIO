using FinanceWalletIOAPI.DTOs.Base;
using FinanceWalletIOAPI.DTOs.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinanceWalletIOAPI.DTOs
{
    public sealed class ExpenseListDto : IApiResult
    {
        public Guid Id { get; set; }
        public string ExpenseType { get; set; } = null!;
        public string Name { get; set; } = null!;  // Custom label for the source (e.g., 'Upwork')
        public string RepeatInterval { get; set; } = null!;
        public string? Notes { get; set; }
    }

    public sealed class ExpenseDetailsDto : IApiResult
    {
        public string ExpenseType { get; set; } = null!;
        public string Name { get; set; } = null!;  // Custom label for the source (e.g., 'Upwork')
        public bool AutoRepeat { get; set; }  // Indicates if it's a repeating income
        public string RepeatInterval { get; set; } = null!;
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public sealed class CreateExpenseDto
    {
        [Required, EnumDataType(typeof(ExpenseStreams))] public ExpenseStreams ExpenseType { get; set; }
        [Required, StringLength(30)] public string Name { get; set; } = null!;  // Custom label for the source (e.g., 'Upwork')
        [Required] public bool AutoRepeat { get; set; }  // Indicates if it's a repeating income        
        [EnumDataType(typeof(TimeInterval))] public TimeInterval RepeatInterval { get; set; } = 0;
        [StringLength(500)] public string? Notes { get; set; }
    }

    public sealed class UpdateExpenseDto
    {
        [Required] public Guid Id { get; set; }
        [Required, EnumDataType(typeof(ExpenseStreams))] public ExpenseStreams ExpenseType { get; set; }
        [Required, StringLength(30)] public string Name { get; set; } = null!;  // Custom label for the source (e.g., 'Upwork')        
        [Required] public bool AutoRepeat { get; set; }  // Indicates if it's a repeating income        
        [EnumDataType(typeof(TimeInterval))] public TimeInterval RepeatInterval { get; set; }
        [StringLength(500)] public string? Notes { get; set; }
    }
}
