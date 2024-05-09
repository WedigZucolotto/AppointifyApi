namespace Appointify.Application.Queries.Events.All
{
    public class GetAllEventsQueryResponse
    {
        public GetAllEventsQueryResponse(
            Guid id,
            string title,
            string? description,
            string date,
            string serviceName)
        {
            Id = id;
            Title = title;
            Description = description;
            Date = date;
            ServiceName = serviceName;
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public string Date { get; set; }

        public string ServiceName { get; set; }
    }
}
