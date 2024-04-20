using System.Collections.ObjectModel;

namespace Appointify.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; set; } = string.Empty;

        public string CompleteName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public Guid CompanyId { get; set; }
        
        public Company Company { get; set; } = new Company();

        public List<Event> Events { get; set; } = new List<Event>();
    }
}
