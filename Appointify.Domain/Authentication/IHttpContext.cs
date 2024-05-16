namespace Appointify.Domain.Authentication
{
    public interface IHttpContext
    {
        Guid? GetUserId();
    }
}
