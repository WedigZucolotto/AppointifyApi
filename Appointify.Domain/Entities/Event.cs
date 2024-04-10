namespace Appointify.Domain.Entities
{
    public class Event : Entity
    {
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public TimeSpan Schedule { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; } = new User();
    }
}
