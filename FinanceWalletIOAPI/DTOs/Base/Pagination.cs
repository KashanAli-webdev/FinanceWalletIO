namespace FinanceWalletIOAPI.DTOs.Base
{
    public class PaginationDto : IApiResult
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<IncomeListDto>? DtoList { get; set; }
    }

}
