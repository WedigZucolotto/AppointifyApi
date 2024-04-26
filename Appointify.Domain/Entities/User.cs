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

        public bool IsAvailable(DateTime initialDate, TimeSpan serviceInterval)
        {
            for (var date = initialDate; date < initialDate + serviceInterval; date += TimeSpan.FromMinutes(1))
            {
                return !Events.Any(e => e.Date == date);
            }
            return true;
        }

        public void SetTypeToEmployee()
        {
            var permissions = new string[]
            {
                "users:getById",
                "users:update",
            };

            Type = UserType.Employee;
            Permissions = permissions
                .Select(permission => new Permission(this, permission)).ToList();
        }

        public void SetTypeToOwner()
        {
            var permissions = new string[]
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

            Type = UserType.Owner;
            Permissions = permissions
                .Select(permission => new Permission(this, permission)).ToList();
        }

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
