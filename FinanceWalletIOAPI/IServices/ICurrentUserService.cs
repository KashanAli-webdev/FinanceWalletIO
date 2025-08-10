namespace FinanceWalletIOAPI.IServices
{
    public interface ICurrentUserService
    {
        public bool IsUserIdEmpty { get; }
        public string? UserId { get; }
        public string? UserName { get; }
    }
}
