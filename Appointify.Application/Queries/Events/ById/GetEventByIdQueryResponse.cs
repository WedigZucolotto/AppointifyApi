namespace Appointify.Application.Queries.Events.ById
{
    public class GetEventByIdQueryResponse
    {
        public GetEventByIdQueryResponse(
            Guid id,
            string title,
            string date, 
            string time, 
            string serviceName)
        {
            Id = id;
            Title = title;
            Date = date;
            Time = time;
            ServiceName = serviceName;
        }

        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Date { get; set; } = string.Empty;

        public string Time { get; set; } = string.Empty;

        public string ServiceName { get; set; } = string.Empty;
    }
}
