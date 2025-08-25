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
    public class ExpenseTransactionController : ControllerBase
    {
        private readonly IExpenseTransactionRepository _outTransactRepo;
        private readonly IResponseService _resServ;
        public ExpenseTransactionController(
            IExpenseTransactionRepository outTransactReop,
            IResponseService resServ)
        {
            _outTransactRepo = outTransactReop;
            _resServ = resServ;
        }


        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var list = await _outTransactRepo.GetAllAsync();

            if (list.OfType<ResponseDto>().Any(r => r.Status == false))
                return Unauthorized(list);

            return Ok(list);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var res = await _outTransactRepo.GetByIdAsync(id);

            if (res is ResponseDto resDto && !resDto.Status)
                return _resServ.HttpRes(this, resDto);

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOutTransactDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _outTransactRepo.CreateAsync(dto);

            if (res is ResponseDto resDto && !resDto.Status)
                return _resServ.HttpRes(this, resDto);

            return Ok(res);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateIncome(Guid id, UpdateOutTransactDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _outTransactRepo.UpdateAsync(id, dto);

            if (res is ResponseDto resDto && !resDto.Status)
                return _resServ.HttpRes(this, resDto);

            return Ok(res);

        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteIncome(Guid id)
        {
            var res = await _outTransactRepo.DeleteAsync(id);

            if (res is ResponseDto resDto && !resDto.Status)
                return _resServ.HttpRes(this, resDto);

            return Ok(res);
        }
    }
}
