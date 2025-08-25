using FinanceWalletIOAPI.Models;

namespace FinanceWalletIOAPI.DTOs.Mappers
{
    public sealed class ExpenseSourceDtoMapper
    {
        public ExpenseListDto ListMap(ExpenseSources expense)
        {
            return new ExpenseListDto
            {
                Id = expense.Id,
                ExpenseType = expense.ExpenseType.ToString(),
                Name = expense.Name,
                RepeatInterval = expense.RepeatInterval.ToString(),
                Notes = expense.Notes
            };
        }

        public ExpenseDetailsDto DetailsMap(ExpenseSources expense)
        {
            return new ExpenseDetailsDto
            {
                ExpenseType = expense.ExpenseType.ToString(),
                Name = expense.Name,
                AutoRepeat = expense.AutoRepeat,
                RepeatInterval = expense.RepeatInterval.ToString(),
                Notes = expense.Notes,
                CreatedAt = expense.CreatedAt
            };
        }

        public ExpenseSources CreateMap(string userId, CreateExpenseDto dto)
        {
            return new ExpenseSources
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ExpenseType = dto.ExpenseType,
                Name = dto.Name,
                AutoRepeat = dto.AutoRepeat,
                RepeatInterval = dto.RepeatInterval,
                Notes = dto.Notes,
                CreatedAt = DateTime.UtcNow
            };
        }

        public ExpenseSources UpdateMap(ExpenseSources expense, UpdateExpenseDto dto)
        {
            expense.Name = dto.Name;
            expense.ExpenseType = dto.ExpenseType;
            expense.AutoRepeat = dto.AutoRepeat;
            expense.RepeatInterval = dto.RepeatInterval;
            expense.Notes = dto.Notes;

            return expense;
        }
    }
}
