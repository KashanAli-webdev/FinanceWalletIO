namespace FinanceWalletIOAPI.DTOs.Base
{
    public class ResponseDto : IApiResult
    {
        public bool Status { get; set; }
        public string? Msg { get; set; }
        public object? Data { get; set; }
    }
}