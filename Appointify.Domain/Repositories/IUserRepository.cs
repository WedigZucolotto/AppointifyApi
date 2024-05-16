using Appointify.Domain.Entities;

namespace Appointify.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByNameAsync(string name);
        Task<User?> GetByIdToDeleteAsync(Guid id);
        Task<List<User>> GetFilteredAsync(Guid? companyId, string? name, string? completeName, bool? isOwner);
    }
}
