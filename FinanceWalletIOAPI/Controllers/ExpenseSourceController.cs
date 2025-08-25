using FinanceWalletIOAPI.DTOs;
using FinanceWalletIOAPI.DTOs.Base;
using FinanceWalletIOAPI.IRepositories;
using FinanceWalletIOAPI.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceWalletIOAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class ExpenseSourceController : ControllerBase
    {
        private readonly IExpenseSourceRepository _expenseRepo;
        private readonly IResponseService _resServ;
        public ExpenseSourceController(
            IExpenseSourceRepository expenseRepo,
            IResponseService resServ)
        {
            _expenseRepo = expenseRepo;
            _resServ = resServ;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var list = await _expenseRepo.GetAllAsync();

            if (list.OfType<ResponseDto>().Any(r => r.Status == false))
                return Unauthorized(list);

            return Ok(list);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var res = await _expenseRepo.GetByIdAsync(id);

            if (res is ResponseDto resDto && !resDto.Status)  // Check if res is ResponseDto than assign a new variable(dto) to the res.
                return _resServ.HttpRes(this, resDto);

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncome(CreateExpenseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _expenseRepo.CreateAsync(dto);

            if (res is ResponseDto resDto && !resDto.Status)  // Check if res is ResponseDto than assign a new variable(dto) to the res.
                return _resServ.HttpRes(this, resDto);

            return Ok(res);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateIncome(Guid id, UpdateExpenseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _expenseRepo.UpdateAsync(id, dto);

            if (res is ResponseDto resDto && !resDto.Status)  // Check if res is ResponseDto than assign a new variable(dto) to the res.
                return _resServ.HttpRes(this, resDto);

            return Ok(res);

        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteIncome(Guid id)
        {
            var res = await _expenseRepo.DeleteAsync(id);

            if (res is ResponseDto resDto && !resDto.Status)  // Check if res is ResponseDto than assign a new variable(dto) to the res.
                return _resServ.HttpRes(this, resDto);

            return Ok(res);
        }
    }
}
