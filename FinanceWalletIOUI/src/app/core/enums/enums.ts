export enum IncomeStreams {
  Salary = 0,
  Business = 1,
  Freelance = 2,
  Contract = 3,
  AssetsRevenue = 4,
  Stocks = 5
}

export enum TimeInterval {
  None = 0,
  Daily = 1,
  Weekly = 7,
  Monthly = 30,
  Annually = 365
}

export enum ExpenseStreams {
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

export enum ResCode {
  OK = 200,
  BadRequest = 400,
  Unauthorized = 401,
  Forbidden = 403,
  NotFound = 404,
  Conflict = 409,
  InternalServerError = 500,
  NotImplemented = 501,
  BadGateway = 502,
  ServiceUnavailable = 503
}