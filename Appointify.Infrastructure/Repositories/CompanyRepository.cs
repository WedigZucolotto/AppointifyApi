using Appointify.Domain.Entities;
using Appointify.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Appointify.Infrastructure.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(DataContext context) : base(context)
        {
        }

        public override Task<List<Company>> GetAllAsync() => 
            Query.Include(c => c.Plan).ToListAsync();
    }
}
