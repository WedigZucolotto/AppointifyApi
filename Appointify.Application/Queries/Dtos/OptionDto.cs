namespace Appointify.Application.Queries.Dtos
{
    public class OptionDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public OptionDto(Guid id, string name) 
        {
            Id = id;
            Name = name;
        }
    }
}
