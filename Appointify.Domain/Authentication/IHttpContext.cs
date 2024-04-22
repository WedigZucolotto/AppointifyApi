using Appointify.Domain.Entities;

namespace Appointify.Domain.Authentication
{
    public interface IHttpContext
    {
        //Task<User?> GetUserAsync();

        Guid? GetUserId();

        bool? HasPermission(string permission);
    }
}
