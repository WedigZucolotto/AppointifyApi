using Appointify.Domain.Entities;

namespace Appointify.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByNameAsync(string name);
    }
}
