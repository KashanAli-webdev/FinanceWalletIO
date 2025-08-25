using FinanceWalletIOAPI.Data;
using FinanceWalletIOAPI.DTOs;
using FinanceWalletIOAPI.DTOs.Base;
using FinanceWalletIOAPI.DTOs.Mappers;
using FinanceWalletIOAPI.IRepositories;
using FinanceWalletIOAPI.IServices;
using FinanceWalletIOAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceWalletIOAPI.Repositories
{
    public class IncomeTransactionRepository : IIncomeTransactionRepository
    {
        private readonly AppDbContext _context;
        private readonly IIncomeSourceRepository _incomeRepo;
        private readonly ICurrentUserService _currentUserServ;
        private readonly IncomeTransactionDtoMapper _dtoMapper;
        private readonly IResponseService _resServ;
        private readonly string _userId;
        public IncomeTransactionRepository(
            AppDbContext context,
            ICurrentUserService currentUserServ,
            IIncomeSourceRepository incomeRepo,
            IncomeTransactionDtoMapper dtoMapper,
            IResponseService resServ)
        {
            _context = context;
            _incomeRepo = incomeRepo;
            _currentUserServ = currentUserServ;
            _userId = _currentUserServ.UserId!;
            _dtoMapper = dtoMapper;
            _resServ = resServ;
        }

        public async Task<IEnumerable<IApiResult>> GetAllAsync()
        {
            if (_currentUserServ.IsUserIdEmpty)
                return new List<ResponseDto>{ _resServ.UnAuthUserRes() };

            return await _context.IncomeTransactions.Where(it => it.UserId == _userId)
                .Join(_context.IncomeSources, 
                    inTransact => inTransact.IncomeSourceId, 
                    income => income.Id, 
                    (inTransact, income) => new { inTransact, income})
                .OrderByDescending(ts => ts.inTransact.ReceivedDate)
                .Select(ts => _dtoMapper.ListMap(ts.income, ts.inTransact)).ToListAsync();
        }


        public async Task<IApiResult> GetByIdAsync(Guid id)
        {
            if (_currentUserServ.IsUserIdEmpty)
                return _resServ.UnAuthUserRes();

            var inTransact = await FindInTransactInDbAsync(id);
            if (inTransact == null)
                return _resServ.NotFoundRes("income transaction");

            var income = await _incomeRepo.FindIncomeInDbAsync(inTransact.IncomeSourceId);
            if (income == null)
                return _resServ.NotFoundRes("income");

            return _dtoMapper.DetailsMap(income, inTransact);
        }

        public async Task<ResponseDto> CreateAsync(CreateInTransactDto dto)
        {
            if (_currentUserServ.IsUserIdEmpty)
                return _resServ.UnAuthUserRes();

            var income = await _incomeRepo.FindIncomeInDbAsync(dto.IncomeSourceId);
            if (income == null)
                return _resServ.NotFoundRes("income");

            var existed = await _context.IncomeTransactions
                .AnyAsync(it => it.UserId == _userId && it.IncomeSourceId == dto.IncomeSourceId && 
                it.Amount == dto.Amount && it.ReceivedDate == dto.ReceivedDate);

            if (existed)
                return _resServ.ConflictRes("income transaction");

            var inTransact = _dtoMapper.CreateMap(_userId, false, dto);
            _context.IncomeTransactions.Add(inTransact);
            await _context.SaveChangesAsync();

            return _resServ.OkRes(
                "income transaction created successfully", _dtoMapper.DetailsMap(income, inTransact));
        }

        public async Task<ResponseDto> UpdateAsync(Guid id, UpdateInTransactDto dto)
        {
            if (_currentUserServ.IsUserIdEmpty)
                return _resServ.UnAuthUserRes();

            if (id != dto.Id)
                return _resServ.BadRequestRes($"id miss matched!");

            var inTransact = await _context.IncomeTransactions
                .FirstOrDefaultAsync(it => it.Id == id && it.UserId == _userId);
            if (inTransact == null)
                return _resServ.NotFoundRes("income transaction");

            var income = await _incomeRepo.FindIncomeInDbAsync(dto.IncomeSourceId);
            if (income == null)
                return _resServ.NotFoundRes("income transaction");

            var existed = await _context.IncomeTransactions
                .AnyAsync(it => it.UserId == _userId && it.IncomeSourceId == dto.IncomeSourceId &&
                it.Amount == dto.Amount && it.ReceivedDate == dto.ReceivedDate && it.Id != id);

            if (existed)
                return _resServ.ConflictRes("income transaction");

            var updated = _dtoMapper.UpdateMap(inTransact, dto);
            await _context.SaveChangesAsync();

            return _resServ.OkRes(
                "income transaction updated successfully", _dtoMapper.DetailsMap(income, inTransact));
        }

        public async Task<ResponseDto> DeleteAsync(Guid id)
        {
            if (_currentUserServ.IsUserIdEmpty)
                return _resServ.UnAuthUserRes();

            var inTransact = await FindInTransactInDbAsync(id);
            if (inTransact == null)
                return _resServ.NotFoundRes("income transaction");

            _context.IncomeTransactions.Remove(inTransact);
            await _context.SaveChangesAsync();

            return _resServ.OkRes("income transaction deleted successfully", null);
        }

        public async Task<IncomeTransaction?> FindInTransactInDbAsync(Guid id)
        {
            return await _context.IncomeTransactions.AsNoTracking()
                .FirstOrDefaultAsync(it => it.Id == id && it.UserId == _userId);
        }
    }
}
