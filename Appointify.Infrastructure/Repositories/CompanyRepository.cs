using Appointify.Domain.Entities;
using Appointify.Domain.Repositories;
using Appointify.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Appointify.Infrastructure.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(DataContext context) : base(context)
        {
        }

        public Task<List<Company>> GetFilteredAsync(string? name, Guid? planId) => 
            Query
                .Include(c => c.Plan)
                .Include(c => c.Users)
                .ConditionalFilter(c => c.Name.ToLower().Contains(name.ToLower()), !string.IsNullOrEmpty(name))
                .ConditionalFilter(c => c.PlanId == planId, planId.HasValue && planId != Guid.Empty)
                .ToListAsync();

        public override Task<Company?> GetByIdAsync(Guid id) => Query
            .Include(c => c.Plan)
            .Include(c => c.Users)
            .Include(c => c.Services)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
