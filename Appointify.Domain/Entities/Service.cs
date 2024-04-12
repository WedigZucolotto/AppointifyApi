using System.Collections.ObjectModel;

namespace Appointify.Domain.Entities
{
    public class Service : Entity
    {
        public string Name { get; set; } = string.Empty;

        public TimeSpan Interval { get; set; }

        public Guid CompanyId { get; set; }

        public Company Company { get; set; } = new Company();

        public ICollection<Event> Events { get; set; } = new Collection<Event>();
    }
}
