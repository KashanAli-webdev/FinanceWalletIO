using FinanceWalletIOAPI.DTOs.Enums;

namespace FinanceWalletIOAPI.DTOs.Requests
{
    public class ListQueryParams<T> where T : struct, Enum
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public T? Category { get; set; }
        public TimeInterval? Interval { get; set; }
    }
}