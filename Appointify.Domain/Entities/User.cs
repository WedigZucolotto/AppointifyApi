using Appointify.Domain.Entities.Enums;

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

        public bool IsAvailable(DateTime initialDate, TimeSpan serviceInterval)
        {
            for (var date = initialDate; date < initialDate + serviceInterval; date += TimeSpan.FromMinutes(1))
            {
                return !Events.Any(e => e.Date == date);
            }
            return true;
        }

        public User() { }

        public User(
            string name, 
            string completeName, 
            string password, 
            UserType type, 
            Company company)
        {
            Name = name;
            CompleteName = completeName;
            Password = password;
            Type = type;
            Company = company;
            CompanyId = company.Id;
        }
    }
}
