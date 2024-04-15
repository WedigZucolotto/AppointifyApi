using Appointify.Domain.Entities;
using Appointify.Domain.Repositories;

namespace Appointify.Infrastructure.Repositories
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        public ServiceRepository(DataContext context) : base(context)
        {
        }
    }
}
