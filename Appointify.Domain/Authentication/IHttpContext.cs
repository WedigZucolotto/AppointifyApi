using Appointify.Domain.Entities.Enums;

namespace Appointify.Domain.Authentication
{
    public interface IHttpContext
    {
        (Guid? Id, UserType? Type) GetUserClaims();
    }
}
