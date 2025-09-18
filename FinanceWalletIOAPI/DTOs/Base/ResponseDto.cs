using FinanceWalletIOAPI.DTOs.Enums;

namespace FinanceWalletIOAPI.DTOs.Base
{
    public sealed class ResponseDto : IApiResult
    {
        public ResCode Code { get; set; }
        public bool Status { get; set; }
        public string? Msg { get; set; }
        public object? Data { get; set; }
    }
}