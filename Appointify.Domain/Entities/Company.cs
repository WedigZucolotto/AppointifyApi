using System.Collections.ObjectModel;

namespace Appointify.Domain.Entities
{
    public class Company : Entity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<User> Users { get; set; } = new Collection<User>();

        public int DayLimit { get; set; }

        public Guid PlanId { get; set; }

        public Plan Plan { get; set; } = new Plan();

        public TimeSpan Open { get; set; }

        public TimeSpan Close { get; set; }

        public List<Event> Events => Users.SelectMany(u => u.Events).ToList();

        public List<Service> Services { get; set; } = new List<Service>();
    }
}
