using Appointify.Domain.Entities.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Appointify.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; set; } = string.Empty;

        public string CompleteName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public UserType Type { get; set; }

        public Guid CompanyId { get; set; }
        
        public Company Company { get; set; } = new Company();

        public List<Event> Events { get; set; } = new List<Event>();

        public List<Permission> Permissions { get; set; } = new List<Permission>();

        public List<TimeSpan> GetAvailableTimes(DateTime initialDate, TimeSpan serviceInterval)
        {
            var availableTimes = new List<TimeSpan>();
            var interval = new TimeSpan();

            for (var time = Company.Open; time < Company.Close; time += TimeSpan.FromMinutes(1))
            {
                var date = initialDate.Add(time);
                var _event = Events.FirstOrDefault(e => e.Date == date);

                if (_event != null)
                {
                    time += _event.Service.Interval;
                    interval = new TimeSpan();
                }

                if (interval == serviceInterval)
                {
                    availableTimes.Add(date.TimeOfDay);
                    interval = new TimeSpan();
                }

                interval += TimeSpan.FromMinutes(1);
            }
            return availableTimes;
        }

        public bool IsAvailable(DateTime initialDate, TimeSpan serviceInterval)
        {
            for (var date = initialDate; date < initialDate + serviceInterval; date += TimeSpan.FromMinutes(1))
            {
                return !Events.Any(e => e.Date == date);
            }
            return true;
        }

        public void SetType(bool isOwner)
        {
            var permissions = isOwner ? OwnerPermissions() : EmployeePermissions();

            Permissions = permissions
                .Select(permission => new Permission(this, permission)).ToList();
            Type = isOwner ? UserType.Owner : UserType.Employee;
        }

        private string[] EmployeePermissions() => new string[] 
        {
            "users:getById",
            "users:update",
        };

        private string[] OwnerPermissions() => new string[]
        {
            "companies:getById",
            "companies:update",
            "services:getAll",
            "services: getById",
            "services:create",
            "services:update",
            "services:delete",
            "users:getAll",
            "users:getById",
            "users:update"
        };

        public User() { }

        public User(
            string name, 
            string completeName, 
            string password,
            Company company)
        {
            Name = name;
            CompleteName = completeName;
            Password = password;
            Company = company;
            CompanyId = company.Id;
        }
    }
}
