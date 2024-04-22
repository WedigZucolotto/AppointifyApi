namespace Appointify.Domain.Entities
{
    public class Permission : Entity
    {
        public Guid UserId { get; set; }

        public User User { get; set; }

        public string Name { get; set; }

        public Permission(User user, string name)
        {
            User = user;
            UserId = user.Id;
            Name = name;
        }

        public Permission() { }
    }
}
