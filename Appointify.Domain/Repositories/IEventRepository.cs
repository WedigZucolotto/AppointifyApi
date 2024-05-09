using Appointify.Domain.Entities;

namespace Appointify.Domain.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<List<Event>> GetFilteredAsync(string? title, DateTime? date, string? serviceName);
    }
}
