namespace FinanceWalletIOAPI.DTOs.Enums
{
    public enum IncomeStreams
    {
        Salary = 0,
        Business = 1,
        Freelance = 2,
        Contract = 3,
        AssetsRevenue = 4,
        Stocks = 5
    }

    public enum IncomeInterval
    {
        None = 0,
        Daily = 1,
        Weekly = 7,
        Monthly = 30,
        Annually = 365
    }
}
