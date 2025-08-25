using FinanceWalletIOAPI.Data;
using FinanceWalletIOAPI.DTOs;
using FinanceWalletIOAPI.DTOs.Base;
using FinanceWalletIOAPI.DTOs.Enums;
using FinanceWalletIOAPI.DTOs.Mappers;
using FinanceWalletIOAPI.IRepositories;
using FinanceWalletIOAPI.IServices;
using FinanceWalletIOAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceWalletIOAPI.Repositories
{
    public class ExpenseSourceRepository : IExpenseSourceRepository
    {
        private readonly AppDbContext _context;
        private readonly ICurrentUserService _currentUserServ;
        private readonly ExpenseSourceDtoMapper _dtoMapper;
        private readonly IResponseService _resServ;
        public ExpenseSourceRepository(
            AppDbContext context,
            ICurrentUserService currentUserServ,
            ExpenseSourceDtoMapper expenseDtoMapper,
            IResponseService resServ)
        {
            _context = context;
            _currentUserServ = currentUserServ;
            _dtoMapper = expenseDtoMapper;
            _resServ = resServ;
        }

        public async Task<IEnumerable<IApiResult>> GetAllAsync()
        {
            if (_currentUserServ.IsUserIdEmpty)
                return new List<ResponseDto> { _resServ.UnAuthUserRes() };

            return await _context.ExpenseSources.Where(e => e.UserId == _currentUserServ.UserId)
                .AsNoTracking().Select(e => _dtoMapper.ListMap(e)).ToListAsync();
        }

        public async Task<IApiResult> GetByIdAsync(Guid id)
        {
            if (_currentUserServ.IsUserIdEmpty)
                return _resServ.UnAuthUserRes();

            var expense = await FindExpenseInDbAsync(id);
            if (expense == null)
                return _resServ.NotFoundRes("expense");

            return _dtoMapper.DetailsMap(expense);
        }

        public async Task<ResponseDto> CreateAsync(CreateExpenseDto dto)
        {
            if (_currentUserServ.IsUserIdEmpty)
                return _resServ.UnAuthUserRes();

            var expenseInterval = ValidateExpenseInterval(dto.AutoRepeat, dto.RepeatInterval);
            if (expenseInterval != null)
                return expenseInterval;

            var existed = await _context.ExpenseSources.AnyAsync(i => i.UserId == _currentUserServ.UserId &&
                i.ExpenseType == dto.ExpenseType && i.Name == dto.Name);
            if (existed)
                return _resServ.ConflictRes("expense");

            var expense = _dtoMapper.CreateMap(_currentUserServ.UserId!, dto);

            _context.ExpenseSources.Add(expense);
            await _context.SaveChangesAsync();

            return _resServ.OkRes("expense created successfully", _dtoMapper.DetailsMap(expense));
        }

        public async Task<ResponseDto> UpdateAsync(Guid id, UpdateExpenseDto dto)
        {
            if (_currentUserServ.IsUserIdEmpty)
                return _resServ.UnAuthUserRes();

            if (id != dto.Id)
                return _resServ.BadRequestRes($"id miss matched!");

            var expense = await _context.ExpenseSources.FirstOrDefaultAsync(i => i.Id == id &&
                i.UserId == _currentUserServ.UserId);

            var expenseInterval = ValidateExpenseInterval(dto.AutoRepeat, dto.RepeatInterval);
            if (expenseInterval != null)
                return expenseInterval;

            if (expense == null)
                return _resServ.NotFoundRes("expense");

            var existed = await _context.ExpenseSources.AnyAsync(i => i.UserId == _currentUserServ.UserId &&
                i.ExpenseType == dto.ExpenseType && i.Name == dto.Name && i.Id != dto.Id);

            if (existed)
                return _resServ.ConflictRes("expense");

            _dtoMapper.UpdateMap(expense, dto);
            await _context.SaveChangesAsync();

            return _resServ.OkRes("expense updated successfully", _dtoMapper.DetailsMap(expense));
        }

        public async Task<ResponseDto> DeleteAsync(Guid id)
        {
            if (_currentUserServ.IsUserIdEmpty)
                return _resServ.UnAuthUserRes();

            var expense = await FindExpenseInDbAsync(id);
            if (expense == null)
                return _resServ.NotFoundRes("expense");

            _context.ExpenseSources.Remove(expense);
            await _context.SaveChangesAsync();

            return _resServ.OkRes("expense deleted successfully", _dtoMapper.DetailsMap(expense));
        }

        public async Task<ExpenseSources?> FindExpenseInDbAsync(Guid id)
        {
            return await _context.ExpenseSources.AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == _currentUserServ.UserId);
        }

        private ResponseDto? ValidateExpenseInterval(bool autoRepeat, TimeInterval interval)
        {
            bool notRepeat = !autoRepeat && interval != TimeInterval.None;
            bool repeat = autoRepeat && interval == TimeInterval.None;
            if (notRepeat || repeat)
                return _resServ.BadRequestRes(
                    $"income interval shouldn't be {interval.ToString()}," +
                    $" if auto repeat is {autoRepeat}");

            return null;
        }
    }
}
