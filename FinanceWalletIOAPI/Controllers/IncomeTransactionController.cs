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
    public class IncomeTransactionController : ControllerBase
    {
        private readonly IIncomeTransactionRepository _inTransactRepo;
        private readonly IResponseService _resServ;
        public IncomeTransactionController(
            IIncomeTransactionRepository inTransactReop,
            IResponseService resServ)
        {
            _inTransactRepo = inTransactReop;
            _resServ = resServ;
        }


        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var list = await _inTransactRepo.GetAllAsync();

            if (list.OfType<ResponseDto>().Any(r => r.Status == false))
                return Unauthorized(list);

            return Ok(list);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var res = await _inTransactRepo.GetByIdAsync(id);

            if (res is ResponseDto resDto && !resDto.Status)
                return _resServ.HttpRes(this, resDto);

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInTransactDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _inTransactRepo.CreateAsync(dto);

            if (res is ResponseDto resDto && !resDto.Status)
                return _resServ.HttpRes(this, resDto);

            return Ok(res);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateIncome(Guid id, UpdateInTransactDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _inTransactRepo.UpdateAsync(id, dto);

            if (res is ResponseDto resDto && !resDto.Status)
                return _resServ.HttpRes(this, resDto);

            return Ok(res);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteIncome(Guid id)
        {
            var res = await _inTransactRepo.DeleteAsync(id);

            if (res is ResponseDto resDto && !resDto.Status)
                return _resServ.HttpRes(this, resDto);

            return Ok(res);
        }
    }
}
