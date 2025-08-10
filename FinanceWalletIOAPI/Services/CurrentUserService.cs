﻿using FinanceWalletIOAPI.IServices;
using System.Security.Claims;

namespace FinanceWalletIOAPI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor) => 
            _httpContextAccessor = httpContextAccessor;

        public string? UserId =>
            _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public string? UserName =>
            _httpContextAccessor.HttpContext?.User?.Identity?.Name;

        public bool IsUserIdEmpty => string.IsNullOrWhiteSpace(UserId);
    }
}
