using FinanceWalletIOAPI.DTOs.Base;
using FinanceWalletIOAPI.DTOs.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinanceWalletIOAPI.DTOs
{
    public sealed class IncomeListDto : IApiResult
    {
        public Guid Id { get; set; }
        public string IncomeType { get; set; } = null!;
        public string Name { get; set; } = null!;  // Custom label for the source (e.g., 'Upwork')
        public string RepeatInterval { get; set; } = null!;
        public string? Notes { get; set; }
    }

    public sealed class IncomeDetailsDto : IApiResult
    {
        public string IncomeType { get; set; } = null!;
        public string Name { get; set; } = null!;  // Custom label for the source (e.g., 'Upwork')
        public bool AutoRepeat { get; set; }  // Indicates if it's a repeating income
        public string RepeatInterval { get; set; } = null!;
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public sealed class CreateIncomeDto
    {
        [Required] public IncomeStreams IncomeType { get; set; }
        [Required, StringLength(30)] public string Name { get; set; } = null!;  // Custom label for the source (e.g., 'Upwork')
        [Required] public bool AutoRepeat { get; set; }  // Indicates if it's a repeating income        
        [EnumDataType(typeof(IncomeInterval))] public IncomeInterval RepeatInterval { get; set; } = 0;        
        [StringLength(500)] public string? Notes { get; set; }
    }

    public sealed class UpdateIncomeDto
    {
        [Required] public Guid Id { get; set; }        
        [Required] public IncomeStreams IncomeType { get; set; }        
        [Required, StringLength(30)] public string Name { get; set; } = null!;  // Custom label for the source (e.g., 'Upwork')        
        [Required] public bool AutoRepeat { get; set; }  // Indicates if it's a repeating income        
        [EnumDataType(typeof(IncomeInterval))] public IncomeInterval RepeatInterval { get; set; }        
        [StringLength(500)] public string? Notes { get; set; }
    }
}
