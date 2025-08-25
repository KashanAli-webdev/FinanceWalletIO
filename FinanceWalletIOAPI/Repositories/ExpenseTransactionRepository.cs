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
    public class ExpenseTransactionRepository : IExpenseTransactionRepository
    {
        private readonly AppDbContext _context;
        private readonly IExpenseSourceRepository _expenseRepo;
        private readonly ICurrentUserService _currentUserServ;
        private readonly ExpenseTransactionDtoMapper _dtoMapper;
        private readonly IResponseService _resServ;
        private readonly string _userId;
        public ExpenseTransactionRepository(
            AppDbContext context,
            ICurrentUserService currentUserServ,
            IExpenseSourceRepository expenseRepo,
            ExpenseTransactionDtoMapper dtoMapper,
            IResponseService resServ)
        {
            _context = context;
            _expenseRepo = expenseRepo;
            _currentUserServ = currentUserServ;
            _userId = _currentUserServ.UserId!;
            _dtoMapper = dtoMapper;
            _resServ = resServ;
        }

        public async Task<IEnumerable<IApiResult>> GetAllAsync()
        {
            if (_currentUserServ.IsUserIdEmpty)
                return new List<ResponseDto> { _resServ.UnAuthUserRes() };

            return await _context.ExpenseTransactions.Where(it => it.UserId == _userId)
                .Join(_context.ExpenseSources,
                    outTransact => outTransact.ExpenseSourceId,
                    expense => expense.Id,
                    (outTransact, expense) => new { outTransact, expense })
                .OrderByDescending(ts => ts.outTransact.DeductDate)
                .Select((ts) => _dtoMapper.ListMap(ts.expense, ts.outTransact)).ToListAsync();
        }


        public async Task<IApiResult> GetByIdAsync(Guid id)
        {
            if (_currentUserServ.IsUserIdEmpty)
                return _resServ.UnAuthUserRes();

            var outTransact = await FindOutTransactInDbAsync(id);
            if (outTransact == null)
                return _resServ.NotFoundRes("expense transaction");

            var expense = await _expenseRepo.FindExpenseInDbAsync(outTransact.ExpenseSourceId);
            if (expense == null)
                return _resServ.NotFoundRes("expense");

            return _dtoMapper.DetailsMap(expense, outTransact);
        }

        public async Task<ResponseDto> CreateAsync(CreateOutTransactDto dto)
        {
            if (_currentUserServ.IsUserIdEmpty)
                return _resServ.UnAuthUserRes();

            var expense = await _expenseRepo.FindExpenseInDbAsync(dto.ExpenseSourceId);
            if (expense == null)
                return _resServ.NotFoundRes("expense");

            var existed = await _context.ExpenseTransactions
                .AnyAsync(it => it.UserId == _userId && it.ExpenseSourceId == dto.ExpenseSourceId &&
                it.Amount == dto.Amount && it.DeductDate == dto.DeductDate);

            if (existed)
                return _resServ.ConflictRes("expense transaction");

            var outTransact = _dtoMapper.CreateMap(_userId, false, dto);
            _context.ExpenseTransactions.Add(outTransact);
            await _context.SaveChangesAsync();

            return _resServ.OkRes(
                "expense transaction created successfully", _dtoMapper.DetailsMap(expense, outTransact));
        }

        public async Task<ResponseDto> UpdateAsync(Guid id, UpdateOutTransactDto dto)
        {
            if (_currentUserServ.IsUserIdEmpty)
                return _resServ.UnAuthUserRes();

            if (id != dto.Id)
                return _resServ.BadRequestRes($"id miss matched!");

            var outTransact = await _context.ExpenseTransactions
                .FirstOrDefaultAsync(it => it.Id == id && it.UserId == _userId);
            if (outTransact == null)
                return _resServ.NotFoundRes("expense transaction");

            var expense = await _expenseRepo.FindExpenseInDbAsync(dto.ExpenseSourceId);
            if (expense == null)
                return _resServ.NotFoundRes("expense transaction");

            var existed = await _context.ExpenseTransactions
                .AnyAsync(it => it.UserId == _userId && it.ExpenseSourceId == dto.ExpenseSourceId &&
                it.Amount == dto.Amount && it.DeductDate == dto.DeductDate && it.Id != id);

            if (existed)
                return _resServ.ConflictRes("expense transaction");

            var updated = _dtoMapper.UpdateMap(outTransact, dto);
            await _context.SaveChangesAsync();

            return _resServ.OkRes(
                "expense transaction updated successfully", _dtoMapper.DetailsMap(expense, outTransact));
        }

        public async Task<ResponseDto> DeleteAsync(Guid id)
        {
            if (_currentUserServ.IsUserIdEmpty)
                return _resServ.UnAuthUserRes();

            var outTransact = await FindOutTransactInDbAsync(id);
            if (outTransact == null)
                return _resServ.NotFoundRes("expense transaction");

            _context.ExpenseTransactions.Remove(outTransact);
            await _context.SaveChangesAsync();

            return _resServ.OkRes("expense transaction deleted successfully", null);
        }

        public async Task<ExpenseTransaction?> FindOutTransactInDbAsync(Guid id)
        {
            return await _context.ExpenseTransactions.AsNoTracking()
                .FirstOrDefaultAsync(it => it.Id == id && it.UserId == _userId);
        }
    }
}
