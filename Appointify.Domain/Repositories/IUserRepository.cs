using Appointify.Domain.Entities;

namespace Appointify.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByNameAsync(string name);

        Task<bool> VerifyCompanyAsync(Guid id, Guid companyId);
    }
}
