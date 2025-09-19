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
    public class IncomeSourceRepository : IIncomeSourceRepository
    {
        private readonly AppDbContext _context;
        private readonly ICurrentUserService _currentUserServ;
        private readonly IncomeSourceDtoMapper _dtoMapper;
        private readonly IResponseService _resServ;
        public IncomeSourceRepository(
            AppDbContext context, 
            ICurrentUserService currentUserServ, 
            IncomeSourceDtoMapper incomeDtoMapper,
            IResponseService resServ)
        {
            _context = context;
            _currentUserServ = currentUserServ;
            _dtoMapper = incomeDtoMapper;
            _resServ = resServ;
        }

        public async Task<IApiResult> GetAllAsync(int pageNum)
        {
            if (_currentUserServ.IsUserIdEmpty)
                return _resServ.UnAuthUserRes();

            if (pageNum < 1) pageNum = 1; // Prevent negative offset

            var baseQuery = _context.IncomeSources
                .Where(i => i.UserId == _currentUserServ.UserId)
                .AsNoTracking();

            int pageSize = 2;
            var totalCount = await baseQuery.CountAsync();

            var dtos = await baseQuery
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .Select(i => _dtoMapper.ListMap(i))
                .ToListAsync();

            return new PaginationDto<IncomeListDto> // Pass Generic type
            {
                DtoList = dtos,
                TotalCount = totalCount,
                PageNumber = pageNum,
                PageSize = pageSize
            };
        }

        public async Task<IApiResult> GetByIdAsync(Guid id)
        {
            if (_currentUserServ.IsUserIdEmpty)
                return _resServ.UnAuthUserRes();

            var income = await FindIncomeInDbAsync(id);
            if (income == null)
                return _resServ.NotFoundRes("income");

            return _dtoMapper.DetailsMap(income);
        }

        public async Task<ResponseDto> CreateAsync(CreateIncomeDto dto)
        {
            if (_currentUserServ.IsUserIdEmpty)
                return _resServ.UnAuthUserRes();

            var incomeInterval = ValidateIncomeInterval(dto.AutoRepeat, dto.RepeatInterval);
            if (incomeInterval != null)
                return incomeInterval;

            var existed = await _context.IncomeSources.AnyAsync(i => i.UserId == _currentUserServ.UserId &&
                i.IncomeType == dto.IncomeType && i.Name == dto.Name);
            if (existed)
                return _resServ.ConflictRes("income");

            var income = _dtoMapper.CreateMap(_currentUserServ.UserId!, dto);

            _context.IncomeSources.Add(income);
            await _context.SaveChangesAsync();

            return _resServ.OkRes("income created successfully", _dtoMapper.DetailsMap(income));
        }

        public async Task<ResponseDto> UpdateAsync(Guid id, UpdateIncomeDto dto)
        {
            if (_currentUserServ.IsUserIdEmpty)
                return _resServ.UnAuthUserRes();

            if (id != dto.Id)
                return _resServ.BadRequestRes($"id miss matched!");

            var income = await _context.IncomeSources.FirstOrDefaultAsync(i => i.Id == id && 
                i.UserId == _currentUserServ.UserId);

            var incomeInterval = ValidateIncomeInterval(dto.AutoRepeat, dto.RepeatInterval);
            if (incomeInterval != null)
                return incomeInterval;

            if (income == null)
                return _resServ.NotFoundRes("income");

            var existed = await _context.IncomeSources.AnyAsync(i => i.UserId == _currentUserServ.UserId && 
                i.IncomeType == dto.IncomeType && i.Name == dto.Name && i.Id != dto.Id);

            if (existed)
                return _resServ.ConflictRes("income");

            _dtoMapper.UpdateMap(income, dto);
            await _context.SaveChangesAsync();

            return _resServ.OkRes("income updated successfully", _dtoMapper.DetailsMap(income));
        }

        public async Task<ResponseDto> DeleteAsync(Guid id)
        {
            if (_currentUserServ.IsUserIdEmpty)
                return _resServ.UnAuthUserRes();

            var income = await FindIncomeInDbAsync(id);
            if (income == null)
                return _resServ.NotFoundRes("income");

            _context.IncomeSources.Remove(income);
            await _context.SaveChangesAsync();

            return _resServ.OkRes("income deleted successfully", _dtoMapper.DetailsMap(income));
        }

        public async Task<IncomeSources?> FindIncomeInDbAsync(Guid id)
        {
            return await _context.IncomeSources.AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == _currentUserServ.UserId);
        }

        private ResponseDto? ValidateIncomeInterval(bool recurring, TimeInterval interval)
        {
            bool notRepeat = !recurring && interval != TimeInterval.None;
            bool repeat = recurring && interval == TimeInterval.None;
            if (notRepeat || repeat)
                return _resServ.BadRequestRes(
                    $"income interval shouldn't be {interval.ToString()}," +
                    $" if auto repeat is {recurring}");

            return null;
        }
    }
}
