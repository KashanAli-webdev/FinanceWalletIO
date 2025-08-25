using FinanceWalletIOAPI.Models;

namespace FinanceWalletIOAPI.DTOs.Mappers
{
    public sealed class ExpenseTransactionDtoMapper
    {
        public OutTransactListDto ListMap(ExpenseSources expense, ExpenseTransaction outTransact)
        {
            return new OutTransactListDto
            {
                Id = outTransact.Id,
                ExpenseName = expense.Name,
                Amount = outTransact.Amount,
                DeductDate = outTransact.DeductDate
            };
        }

        public OutTransactDetailsDto DetailsMap(ExpenseSources expense, ExpenseTransaction outTransact)
        {
            return new OutTransactDetailsDto
            {
                ExpenseType = expense.ExpenseType.ToString(),
                ExpenseName = expense.Name,
                Amount = outTransact.Amount,
                DeductDate = outTransact.DeductDate,
                Notes = outTransact.Notes,
                IsAutoAdded = outTransact.IsAutoAdded,
                CreatedAt = outTransact.CreatedAt
            };
        }

        public ExpenseTransaction CreateMap(string userId, bool isAutoAdded, CreateOutTransactDto dto)
        {
            return new ExpenseTransaction
            {
                Id = Guid.NewGuid(),
                ExpenseSourceId = dto.ExpenseSourceId,
                UserId = userId,
                Amount = dto.Amount,
                DeductDate = dto.DeductDate,
                Notes = dto.Notes,
                IsAutoAdded = isAutoAdded,
                CreatedAt = DateTime.UtcNow
            };
        }

        public ExpenseTransaction UpdateMap(ExpenseTransaction outTransact, UpdateOutTransactDto dto)
        {
            outTransact.ExpenseSourceId = dto.ExpenseSourceId;
            outTransact.Amount = dto.Amount;
            outTransact.DeductDate = dto.DeductDate;
            outTransact.Notes = dto.Notes;

            return outTransact;
        }
    }
}
