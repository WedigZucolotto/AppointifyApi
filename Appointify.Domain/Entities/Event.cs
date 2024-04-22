namespace Appointify.Domain.Entities
{
    public class Event : Entity
    {
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime Date { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; } = new User();

        public Guid ServiceId { get; set; }

        public Service Service { get; set; } = new Service();

        public Event(
            string title, 
            string? description,
            DateTime date, 
            User user, 
            Service service)
        {
            Title = title;
            Description = description;
            Date = date;
            User = user;
            UserId = user.Id;
            Service = service;
            ServiceId = service.Id;
        }

        public Event() { }
    }
}
