namespace Appointify.Domain
{
    public interface IUnitOfWork
    {
        public Task CommitAsync();
    }
}
