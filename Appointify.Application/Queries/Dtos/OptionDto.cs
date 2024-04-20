namespace Appointify.Application.Queries.Dtos
{
    public class OptionDto
    {
        public OptionDto(string name, Guid id)
        {
            Name = name;
            Value = id;
        }

        public string Name { get; set; } = string.Empty;

        public Guid Value { get; set; }
    }
}
