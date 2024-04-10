namespace Appointify.Domain.Entities
{
    public class Entity
    {
        public Guid Id { get; protected set; }

        public DateTime CreatedAt { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime ModifiedAt { get; set; }

        public Guid ModifiedBy { get; set; }
    }
}
