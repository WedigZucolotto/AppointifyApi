using Appointify.Domain.Entities;
using Appointify.Domain.Repositories;

namespace Appointify.Infastructure.Repositories
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(DataContext context) : base(context)
        {
        }
    }
}
