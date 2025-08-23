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

    public enum ExpenseStreams
    {
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
