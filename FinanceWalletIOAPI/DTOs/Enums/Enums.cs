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

    public enum TimeInterval
    {
        None = 0,
        Daily = 1,
        Weekly = 2,
        Monthly = 3,
        Annually = 4
    }

    public enum ExpenseStreams
    {
        Housing = 0,
        Utilities = 1,
        Transportation = 2,
        Food = 3,
        Healthcare = 4,
        Insurance = 5,
        PersonalCare = 6,
        Family = 7,
        Entertainment = 8,
        DebtPayments = 9,
        Savings = 10,
        Investments = 11,
        Education = 12,
        Social = 13,
        Donations = 14,
        Travel = 15,
        Apparel = 16,
        Taxes = 17,
        Productivity = 18,
        Subscriptions = 19,
        Miscellaneous = 20
    }

    public enum ResCode
    {
        // 2xx Success
        OK = 200,                // Standard success

        // 4xx Client Errors
        BadRequest = 400,       // Invalid request from client
        Unauthorized = 401,     // Not authenticated or invalid token
        Forbidden = 403,        // Authenticated but has no permission
        NotFound = 404,         // Resource not found
        Conflict = 409,         // Conflict with current state (duplicate data, etc.)

        // 5xx Server Errors
        InternalServerError = 500, // Generic server-side error
        NotImplemented = 501,   // Not implemented/unsupported
        BadGateway = 502,       // Invalid response from upstream server
        ServiceUnavailable = 503 // Server overloaded or under maintenance
    }
}
