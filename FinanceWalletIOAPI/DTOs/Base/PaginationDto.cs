namespace FinanceWalletIOAPI.DTOs.Base
{
    public class PaginationDto<T> : IApiResult // declear Generic type.
    {
        public List<T>? DtoList { get; set; } // use Generic type.
        public int TotalCount { get; set; }
        public int PageNum { get; set; }
        public int PageSize { get; set; }
    }
}