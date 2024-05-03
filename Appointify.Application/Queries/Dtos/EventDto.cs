namespace Appointify.Application.Queries.Dtos
{
    public class EventDto
    {
        public EventDto(Guid id, string title, string time)
        {
            Id = id;
            Title = title;
            Time = time;
        }

        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Time { get; set; } = string.Empty;
    }
}
