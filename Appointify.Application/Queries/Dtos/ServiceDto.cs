namespace Appointify.Application.Queries.Dtos
{
    public class ServiceDto
    {
        public ServiceDto(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
