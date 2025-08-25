using FinanceWalletIOAPI.DTOs;
using FinanceWalletIOAPI.DTOs.Base;
using FinanceWalletIOAPI.Models;

namespace FinanceWalletIOAPI.IRepositories
{
    public interface IExpenseSourceRepository
    {
        public Task<IEnumerable<IApiResult>> GetAllAsync();
        public Task<IApiResult> GetByIdAsync(Guid id);
        public Task<ResponseDto> CreateAsync(CreateExpenseDto dto);
        public Task<ResponseDto> UpdateAsync(Guid id, UpdateExpenseDto dto);
        public Task<ResponseDto> DeleteAsync(Guid id);
        public Task<ExpenseSources?> FindExpenseInDbAsync(Guid id);
    }
}
