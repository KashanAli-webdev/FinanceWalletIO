using FinanceWalletIOAPI.DTOs.Base;
using FinanceWalletIOAPI.DTOs.Enums;
using FinanceWalletIOAPI.IServices;
using Microsoft.AspNetCore.Mvc;

namespace FinanceWalletIOAPI.Services
{
    public class ResponseService : IResponseService
    {
        public ResponseDto UnAuthUserRes()
        {
            return new ResponseDto 
            {
                Code = ResCode.Unauthorized,
                Status = false, 
                Msg = "User is not authenticated!"
            };
        }

        public ResponseDto NotFoundRes(string objName)
        {
            return new ResponseDto 
            {
                Code = ResCode.NotFound,
                Status = false, 
                Msg = $"{objName} not found!"
            };
        }

        public ResponseDto BadRequestRes(string msg)
        {
            return new ResponseDto
            {
                Code = ResCode.BadRequest,
                Status = false,
                Msg = $"{msg}"
            };
        }

        public ResponseDto ConflictRes(string objName)
        {
            return new ResponseDto
            {
                Code = ResCode.Conflict,
                Status = false,
                Msg = $"This {objName} already exist!"
            };
        }

        public ResponseDto OkRes(string msg, object? obj)
        {
            return new ResponseDto 
            { 
                Code = ResCode.OK,
                Status = true, 
                Msg = msg, 
                Data = obj 
            };
        }

        public IActionResult HttpRes(ControllerBase controller, ResponseDto res)
        {
            return res.Code switch
            {
                ResCode.Unauthorized => controller.Unauthorized(res),
                ResCode.NotFound => controller.NotFound(res),
                ResCode.Conflict => controller.Conflict(res),
                _ => controller.BadRequest(res)
            };
        }
    }
}
