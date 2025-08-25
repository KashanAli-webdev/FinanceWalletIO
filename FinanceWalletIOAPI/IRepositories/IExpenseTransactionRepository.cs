using FinanceWalletIOAPI.DTOs;
using FinanceWalletIOAPI.DTOs.Base;
using FinanceWalletIOAPI.Models;

namespace FinanceWalletIOAPI.IRepositories
{
    public interface IExpenseTransactionRepository
    {
        public Task<IEnumerable<IApiResult>> GetAllAsync();
        public Task<IApiResult> GetByIdAsync(Guid id);
        public Task<ResponseDto> CreateAsync(CreateOutTransactDto dto);
        public Task<ResponseDto> UpdateAsync(Guid id, UpdateOutTransactDto dto);
        public Task<ResponseDto> DeleteAsync(Guid id);
        public Task<ExpenseTransaction?> FindOutTransactInDbAsync(Guid id);
    }
}
