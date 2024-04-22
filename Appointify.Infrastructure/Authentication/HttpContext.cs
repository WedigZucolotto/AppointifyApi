using Appointify.Domain.Authentication;
using Appointify.Domain.Entities.Enums;
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

        public (Guid? Id, UserType? Type) GetUserClaims()
        {
            var userId = _httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userType = _httpContextAccessor?.HttpContext?.User.FindFirst("userType")?.Value;

            var id = userId != null ? (Guid?)Guid.Parse(userId) : null;
            var type = userType != null ? (UserType?)Enum.Parse(typeof(UserType), userType) : null;

            return (id, type);
        }
    }
}
