using FinanceWalletIOAPI.Models;

namespace FinanceWalletIOAPI.DTOs.Mappers
{
    public sealed class IncomeSourceDtoMapper
    {
        public IncomeListDto ListMap(IncomeSources income)
        {
            return new IncomeListDto
            {
                Id = income.Id,
                IncomeType = income.IncomeType.ToString(),
                Name = income.Name,
                RepeatInterval = income.RepeatInterval.ToString(),
            };
        }

        public IncomeDetailsDto DetailsMap(IncomeSources income)
        {
            return new IncomeDetailsDto
            {
                IncomeType = income.IncomeType.ToString(),
                Name = income.Name,
                AutoRepeat = income.AutoRepeat,
                RepeatInterval = income.RepeatInterval.ToString(),
                Notes = income.Notes,
                CreatedAt = income.CreatedAt
            };
        }

        public IncomeSources CreateMap(string userId, CreateIncomeDto dto)
        {
            return new IncomeSources
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                IncomeType = dto.IncomeType,
                Name = dto.Name,
                AutoRepeat = dto.AutoRepeat,
                RepeatInterval = dto.RepeatInterval,
                Notes = dto.Notes,
                CreatedAt = DateTime.UtcNow
            };
        }

        public IncomeSources UpdateMap(IncomeSources income, UpdateIncomeDto dto)
        {
            income.Name = dto.Name;
            income.IncomeType = dto.IncomeType;
            income.AutoRepeat = dto.AutoRepeat;
            income.RepeatInterval = dto.RepeatInterval;
            income.Notes = dto.Notes;

            return income;
        }
    }
}
