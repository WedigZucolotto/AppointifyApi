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

        public override Task<Event?> GetByIdAsync(Guid id) =>
            Query
                .Include(e => e.Service)
                .FirstOrDefaultAsync(e => e.Id == id);

        public Task<List<Event>> GetFilteredAsync(string? title, DateTime? date, string? serviceName, Guid? userId, Guid? companyId) =>
            Query
                .Include(e => e.Service)
                .Include(e => e.User)
                .ConditionalFilter(e => e.Title.ToLower().Contains(title.ToLower()), !string.IsNullOrEmpty(title))
                .ConditionalFilter(e => e.Service.Name.ToLower().Contains(serviceName.ToLower()), !string.IsNullOrEmpty(serviceName))
                .ConditionalFilter(e => e.Date.Date.Equals(date.Value.Date), date != null)
                .ConditionalFilter(e => e.UserId.Equals(userId), userId != Guid.Empty)
                .ConditionalFilter(e => e.User.CompanyId.Equals(companyId), companyId != Guid.Empty)
                .ToListAsync();
    }
}
