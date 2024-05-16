namespace Appointify.Domain.Entities
{
    public class Permission : Entity
    {
        public Guid UserId { get; set; }

        public User User { get; set; }

        public string Name { get; set; }

        public Permission() { }
    }
}
