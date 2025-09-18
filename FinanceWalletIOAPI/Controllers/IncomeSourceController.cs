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
    public class IncomeSourceController : ControllerBase
    {
        private readonly IIncomeSourceRepository _incomeRepo;
        private readonly IResponseService _resServ;
        public IncomeSourceController(
            IIncomeSourceRepository incomeSourceRepo,
            IResponseService resServ)
        {
            _incomeRepo = incomeSourceRepo;
            _resServ = resServ;
        }


        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int pageNum)
        {
            var res = await _incomeRepo.GetAllAsync(pageNum);

            if (res is ResponseDto resDto && !resDto.Status)  // Check if res is ResponseDto than assign a new variable(dto) to the res.
                return _resServ.HttpRes(this, resDto);

            return Ok(res);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetList([FromQuery] int pageNum)
        //{
        //    var list = await _incomeRepo.GetAllAsync(pageNum);

        //    if (list.OfType<ResponseDto>().Any(r => r.Status == false))
        //        return Unauthorized(list);

        //    return Ok(list);
        //}

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var res = await _incomeRepo.GetByIdAsync(id);

            if (res is ResponseDto resDto && !resDto.Status)  // Check if res is ResponseDto than assign a new variable(dto) to the res.
                return _resServ.HttpRes(this, resDto);

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncome(CreateIncomeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _incomeRepo.CreateAsync(dto);

            if (res is ResponseDto resDto && !resDto.Status)  // Check if res is ResponseDto than assign a new variable(dto) to the res.
                return _resServ.HttpRes(this, resDto);

            return Ok(res);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateIncome(Guid id, UpdateIncomeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _incomeRepo.UpdateAsync(id, dto);

            if (res is ResponseDto resDto && !resDto.Status)  // Check if res is ResponseDto than assign a new variable(dto) to the res.
                return _resServ.HttpRes(this, resDto);

            return Ok(res);

        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteIncome(Guid id) 
        {
            var res = await _incomeRepo.DeleteAsync(id);

            if (res is ResponseDto resDto && !resDto.Status)  // Check if res is ResponseDto than assign a new variable(dto) to the res.
                return _resServ.HttpRes(this, resDto);

            return Ok(res); 
        }
    }
}
