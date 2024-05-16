using Appointify.Domain.Entities.Dtos;
using System.Collections.ObjectModel;

namespace Appointify.Domain.Entities
{
    public class Company : Entity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<User> Users { get; set; } = new Collection<User>();

        public Guid PlanId { get; set; }

        public Plan Plan { get; set; }

        public TimeSpan Open { get; set; }

        public TimeSpan Close { get; set; }

        public List<Service> Services { get; set; } = new List<Service>();

        public List<Event> GetEvents() => Users.SelectMany(u => u.Events).ToList();

        public bool IsOwner(Guid? userId) => 
            Users.Any(u => u.Id == userId && u.IsOwner);

        public List<AvailableTimeDto> GetAvailableTimes(DateTime date, TimeSpan serviceInterval)
        {
            var availableTimes = new List<AvailableTimeDto>();

            foreach (var user in Users)
            {
                var userTimes = user.GetAvailableTimes(date, serviceInterval);

                foreach (var time in userTimes)
                {
                    var timeExists = availableTimes.Any(at => at.Time == time);
                    
                    if (!timeExists)
                    {
                        availableTimes.Add(new AvailableTimeDto(time, user.Id));
                    }
                }
            }

            return availableTimes;
        }

        public Company() { }
    }
}
