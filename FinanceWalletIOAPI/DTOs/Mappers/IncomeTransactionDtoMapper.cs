using FinanceWalletIOAPI.Models;

namespace FinanceWalletIOAPI.DTOs.Mappers
{
    public sealed class IncomeTransactionDtoMapper
    {
        public InTransactListDto ListMap(IncomeSources income, IncomeTransaction inTransact)
        {
            return new InTransactListDto
            {
                Id = inTransact.Id,
                IncomeName = income.Name,
                Amount = inTransact.Amount,
                ReceivedDate = inTransact.ReceivedDate
            };
        }

        public InTransactDetailsDto DetailsMap(IncomeSources income, IncomeTransaction inTransact)
        {
            return new InTransactDetailsDto
            {
                IncomeType = income.IncomeType.ToString(),
                IncomeName = income.Name,
                Amount = inTransact.Amount,
                ReceivedDate = inTransact.ReceivedDate,
                Notes = inTransact.Notes,
                IsAutoAdded = inTransact.IsAutoAdded,
                CreatedAt = inTransact.CreatedAt
            };
        }

        public IncomeTransaction CreateMap(string userId, bool isAutoAdded, CreateInTransactDto dto)
        {
            return new IncomeTransaction
            {
                Id = Guid.NewGuid(),
                IncomeSourceId = dto.IncomeSourceId,
                UserId = userId,
                Amount = dto.Amount,
                ReceivedDate = dto.ReceivedDate,
                Notes = dto.Notes,
                IsAutoAdded = isAutoAdded,
                CreatedAt = DateTime.UtcNow
            };
        }

        public IncomeTransaction UpdateMap(IncomeTransaction inTransact, UpdateInTransactDto dto)
        {
            inTransact.IncomeSourceId = dto.IncomeSourceId;
            inTransact.Amount = dto.Amount;
            inTransact.ReceivedDate = dto.ReceivedDate;
            inTransact.Notes = dto.Notes;

            return inTransact;
        }
    }
}
