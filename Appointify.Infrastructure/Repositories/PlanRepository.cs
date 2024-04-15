using Appointify.Domain.Entities;
using Appointify.Domain.Repositories;

namespace Appointify.Infrastructure.Repositories
{
    public class PlanRepository : Repository<Plan>, IPlanRepository
    {
        public PlanRepository(DataContext context) : base(context)
        {
        }
    }
}
