using FinanceWalletIOAPI.Models;

namespace FinanceWalletIOAPI.DTOs.Mappers
{
    public class IncomeSourceDtoMapper
    {
        public IncomeListDto ListMap(IncomeSources income)
        {
            return new IncomeListDto
            {
                Id = income.Id,
                IncomeType = income.IncomeType,
                Name = income.Name,
                RecurringInterval = income.RecurringInterval,
                Notes = income.Notes
            };
        }

        public IncomeDetailsDto DetailsMap(IncomeSources income)
        {
            return new IncomeDetailsDto
            {
                IncomeType = income.IncomeType,
                Name = income.Name,
                IsRecurring = income.IsRecurring,
                RecurringInterval = income.RecurringInterval,
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
                IsRecurring = dto.IsRecurring,
                RecurringInterval = dto.IsRecurring ? dto.RecurringInterval : 0,
                Notes = dto.Notes,
                CreatedAt = DateTime.UtcNow
            };
        }

        public IncomeSources UpdateMap(IncomeSources income, UpdateIncomeDto dto)
        {
            income.Name = dto.Name;
            income.IncomeType = dto.IncomeType;
            income.IsRecurring = dto.IsRecurring;
            income.RecurringInterval = dto.IsRecurring ? dto.RecurringInterval : 0;
            income.Notes = dto.Notes;

            return income;
        }
    }
}
