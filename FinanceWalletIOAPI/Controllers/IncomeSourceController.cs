using FinanceWalletIOAPI.DTOs;
using FinanceWalletIOAPI.DTOs.Base;
using FinanceWalletIOAPI.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceWalletIOAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class IncomeSourceController : ControllerBase
    {
        private readonly IIncomeSourceRepository _incomeSourceRepo;
        public IncomeSourceController(IIncomeSourceRepository incomeSourceRepo) 
            => _incomeSourceRepo = incomeSourceRepo;

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var list = await _incomeSourceRepo.GetAllAsync();

            if (list.OfType<ResponseDto>().Any(r => r.Status == false))
                return Unauthorized(list);

            return Ok(list);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var res = await _incomeSourceRepo.GetByIdAsync(id);

            if (res == null)
                return NotFound(res);

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncome(CreateIncomeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _incomeSourceRepo.CreateAsync(dto);

            if (res.Status == false)
                return Conflict(res);

            return Ok(res);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateIncome(Guid id, UpdateIncomeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _incomeSourceRepo.UpdateAsync(id, dto);

            if (res.Status == false)
                return Conflict(res);

            return Ok(res);

        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteIncome(Guid id) 
        {
            var res = await _incomeSourceRepo.DeleteAsync(id);

            if (res.Status == false)
                return NotFound(res);

            return Ok(res); 
        }
    }
}
