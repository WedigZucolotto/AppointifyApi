using Appointify.Domain.Entities;

namespace Appointify.Domain.Repositories
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<List<Company>> GetFilteredAsync(string? name, Guid? planId);
    }
}
