using Appointify.Domain.Entities;
using Appointify.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Appointify.Infrastructure.Repositories
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        public ServiceRepository(DataContext context) : base(context)
        {
        }

        public Task<List<Service>> GetAllFilteredByCompanyAsync(Guid companyId) =>
            Query.Where(s => s.CompanyId == companyId).ToListAsync();
    }
}
