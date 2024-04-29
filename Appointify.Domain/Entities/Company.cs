using Appointify.Domain.Entities.Dtos;
using Appointify.Domain.Entities.Enums;
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

        public List<Event> Events() => Users.SelectMany(u => u.Events).ToList();

        public bool CanEdit((Guid? Id, UserType? Type) user)
        {
            var isOwnerUser = Users
                .Where(u => u.Type is UserType.Owner)
                .Any(u => u.Id == user.Id);

            return isOwnerUser || user.Type == UserType.Admin;
        }

        public bool IsStandard() => Plan.Name == "Standard";

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

        public Company(
            string name,
            Plan plan, 
            TimeSpan open, 
            TimeSpan close)
        {
            Name = name;
            Plan = plan;
            PlanId = plan.Id;
            Open = open;
            Close = close;
        }

        public Company() { }
    }
}
