using Appointify.Domain.Entities;
using Appointify.Domain.Repositories;
using Appointify.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Appointify.Infrastructure.Repositories
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        public ServiceRepository(DataContext context) : base(context)
        {
        }

        public Task<List<Service>> GetFilteredAsync(Guid? companyId, string? name) =>
            Query
                .Include(s => s.Company)
                .ConditionalFilter(s => s.CompanyId == companyId, companyId.HasValue && companyId != Guid.Empty)
                .ConditionalFilter(s => s.Name.ToLower().Contains(name.ToLower()), !string.IsNullOrEmpty(name))
                .ToListAsync();

        public override Task<Service?> GetByIdAsync(Guid id) =>
            Query
                .Include(s => s.Company).ThenInclude(c => c.Users)
                .FirstOrDefaultAsync(s => s.Id == id);
    }
}
