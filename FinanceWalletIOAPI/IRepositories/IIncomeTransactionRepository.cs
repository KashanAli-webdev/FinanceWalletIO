using FinanceWalletIOAPI.DTOs;
using FinanceWalletIOAPI.DTOs.Base;
using FinanceWalletIOAPI.Models;

namespace FinanceWalletIOAPI.IRepositories
{
    public interface IIncomeTransactionRepository
    {
        public Task<IEnumerable<IApiResult>> GetAllAsync();
        public Task<IApiResult> GetByIdAsync(Guid id);
        public Task<ResponseDto> CreateAsync(CreateInTransactDto dto);
        public Task<ResponseDto> UpdateAsync(Guid id, UpdateInTransactDto dto);
        public Task<ResponseDto> DeleteAsync(Guid id);
        public Task<IncomeTransaction?> FindInTransactInDbAsync(Guid id);
    }
}
