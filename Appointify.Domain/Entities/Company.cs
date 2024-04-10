namespace Appointify.Domain.Entities
{
    public class Company : Entity
    {
        public string Name { get; set; } = string.Empty;

        public TimeSpan Interval { get; set; }

        public IEnumerable<User> Users { get; set; }
    }
}
