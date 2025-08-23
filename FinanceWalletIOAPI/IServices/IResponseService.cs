using FinanceWalletIOAPI.DTOs.Base;
using Microsoft.AspNetCore.Mvc;

namespace FinanceWalletIOAPI.IServices
{
    public interface IResponseService
    {
        public ResponseDto UnAuthUserRes();
        public ResponseDto NotFoundRes(string msg);
        public ResponseDto BadRequestRes(string msg);
        public ResponseDto ConflictRes(string objName);
        public ResponseDto OkRes(string msg, object? obj);
        public IActionResult HttpRes(ControllerBase controller, ResponseDto res);
    }
}
