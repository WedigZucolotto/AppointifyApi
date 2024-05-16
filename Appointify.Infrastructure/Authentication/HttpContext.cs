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

        public Guid? GetUserId()
        {
            var userId = _httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId != null ? (Guid?)Guid.Parse(userId) : null;
        }
    }
}
