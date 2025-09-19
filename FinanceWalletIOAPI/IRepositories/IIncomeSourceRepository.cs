using FinanceWalletIOAPI.DTOs;
using FinanceWalletIOAPI.DTOs.Base;
using FinanceWalletIOAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceWalletIOAPI.IRepositories
{
    public interface IIncomeSourceRepository
    {
        public Task<IApiResult> GetAllAsync(int pageNum, int pageSize);
        public Task<IApiResult> GetByIdAsync(Guid id);
        public Task<ResponseDto> CreateAsync(CreateIncomeDto dto);
        public Task<ResponseDto> UpdateAsync(Guid id, UpdateIncomeDto dto);
        public Task<ResponseDto> DeleteAsync(Guid id);
        public Task<IncomeSources?> FindIncomeInDbAsync(Guid id);
    }
}
