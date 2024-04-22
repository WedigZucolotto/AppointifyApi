using Appointify.Domain.Entities;

namespace Appointify.Domain.Authentication
{
    public interface IJwtProvider
    {
        string Generate(User user);
    }
}
