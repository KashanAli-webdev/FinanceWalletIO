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
    public class IncomeSourceRepository : IIncomeSourceRepository
    {
        private readonly AppDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IncomeSourceDtoMapper _incomeDtoMapper;
        public IncomeSourceRepository(
            AppDbContext context, 
            ICurrentUserService currentUser, 
            IncomeSourceDtoMapper incomeDtoMapper)
        {
            _context = context;
            _currentUserService = currentUser;
            _incomeDtoMapper = incomeDtoMapper;
        }


        public async Task<IEnumerable<IApiResult>> GetAllAsync()
        {
            if (_currentUserService.IsUserIdEmpty)
                return new List<ResponseDto>
                {
                    new ResponseDto
                    {
                        Status = false,
                        Msg = "User is not authenticated!"
                    }
                }; 
            
            return await _context.IncomeSources.Where(i => i.UserId == _currentUserService.UserId).AsNoTracking()
                .Select(i => _incomeDtoMapper.ListMap(i)).ToListAsync();
        }

        public async Task<IApiResult> GetByIdAsync(Guid id)
        {
            var income = await FindIncomeInDbAsync(id);
            if (income == null)
                return new ResponseDto
                {
                    Status = false,
                    Msg = $"{id}: income Id not exists in db..",
                };
            return _incomeDtoMapper.DetailsMap(income);
        }

        public async Task<ResponseDto> CreateAsync(CreateIncomeDto dto)
        {
            if (_currentUserService.IsUserIdEmpty)
                return new ResponseDto
                {
                    Status = false,
                    Msg = $"User is not authenticated!"
                };

            var existed = await _context.IncomeSources.AnyAsync(i => i.UserId == _currentUserService.UserId &&
                i.IncomeType == dto.IncomeType && i.Name == dto.Name);

            if (existed)
                return new ResponseDto
                {
                    Status = false,
                    Msg = $"Income Source already exist with {dto.Name} and {dto.IncomeType}",
                };

            var income = _incomeDtoMapper.CreateMap(_currentUserService.UserId!, dto);

            _context.IncomeSources.Add(income);
            await _context.SaveChangesAsync();

            return new ResponseDto
            {
                Status = true,
                Msg = $"{income.Name} income successfully created!",
                Data = _incomeDtoMapper.DetailsMap(income)
            };
        }

        public async Task<ResponseDto> UpdateAsync(Guid id, UpdateIncomeDto dto)
        {
            if (id != dto.Id)
                return new ResponseDto
                {
                    Status = false,
                    Msg = $"Route id: {id} not match with reqested model id: {dto.Id}!"
                };

            var income = await _context.IncomeSources.FirstOrDefaultAsync(i => i.Id == id && 
                i.UserId == _currentUserService.UserId);

            if (income == null)
                return new ResponseDto
                {
                    Status = false,
                    Msg = $"The Income with this id: {id} not exist!"
                };

            var existed = await _context.IncomeSources.AnyAsync(i => i.UserId == _currentUserService.UserId && 
                i.IncomeType == dto.IncomeType && i.Name == dto.Name && i.Id != dto.Id);

            if (existed)
                return new ResponseDto
                {
                    Status = false,
                    Msg = $"Income Source already exist with {dto.Name} and {dto.IncomeType}",
                };
            _incomeDtoMapper.UpdateMap(income, dto);
            await _context.SaveChangesAsync();
            return new ResponseDto
            {
                Status = true,
                Msg = $"{income.Name} income successfully updated!",
                Data = _incomeDtoMapper.DetailsMap(income)
            };
        }

        public async Task<ResponseDto> DeleteAsync(Guid id)
        {
            var income = await FindIncomeInDbAsync(id);
            if (income == null)
                return new ResponseDto
                {
                    Status = false,
                    Msg = $"The Income with this id: {id} not exist!"
                }; 
            
            _context.IncomeSources.Remove(income);
            await _context.SaveChangesAsync();
            
            return new ResponseDto
            {
                Status = true,
                Msg = $"{income.Name} income successfully deleted.",
                Data = _incomeDtoMapper.DetailsMap(income)
            };
        }

        public async Task<IncomeSources?> FindIncomeInDbAsync(Guid id)
        {
            return await _context.IncomeSources.AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == _currentUserService.UserId);
        }
    }
}
