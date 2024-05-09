using Appointify.Domain.Entities;
using Appointify.Domain.Repositories;
using Appointify.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Appointify.Infrastructure.Repositories
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(DataContext context) : base(context)
        {
        }

        public Task<List<Event>> GetFilteredAsync(string? title, DateTime? date, string? serviceName) =>
            Query
                .Include(e => e.Service)
                .ConditionalFilter(e => e.Title.ToLower().Contains(title.ToLower()), !string.IsNullOrEmpty(title))
                .ConditionalFilter(e => e.Date.Date.Equals(date), date != null)
                .ConditionalFilter(e => e.Service.Name.ToLower().Contains(serviceName.ToLower()), !string.IsNullOrEmpty(serviceName))
                .ToListAsync();
    }
}
