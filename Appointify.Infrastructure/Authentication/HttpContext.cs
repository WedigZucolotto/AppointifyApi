using Appointify.Domain.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Appointify.Infrastructure.Authentication
{
    public class HttpContext : IHttpContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? GetUserId() => GetPrivateUserId();

        public bool? HasPermission(string permission)
        {
            return _httpContextAccessor?.HttpContext?.User
                .FindAll("permissions")
                .Select(permission => permission.Value)
                .Contains(permission);
        }

        private Guid? GetPrivateUserId()
        {
            var userId = _httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId != null ? Guid.Parse(userId) : null;
        }
    }
}
