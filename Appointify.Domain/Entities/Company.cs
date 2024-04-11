using System.Collections.ObjectModel;

namespace Appointify.Domain.Entities
{
    public class Company : Entity
    {
        public string Name { get; set; } = string.Empty;

        public TimeSpan Interval { get; set; }

        public ICollection<User> Users { get; set; } = new Collection<User>();
    }
}
