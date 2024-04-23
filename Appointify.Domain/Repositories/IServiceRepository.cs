using Appointify.Domain.Entities;

namespace Appointify.Domain.Repositories
{
    public interface IServiceRepository : IRepository<Service>
    {
        Task<List<Service>> GetFilteredAsync(Guid? companyId, string? name);
    }
}
