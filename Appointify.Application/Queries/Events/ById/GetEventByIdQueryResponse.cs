namespace Appointify.Application.Queries.Events.ById
{
    public class GetEventByIdQueryResponse
    {
        public GetEventByIdQueryResponse(
            string title, 
            string description, 
            string date, 
            string serviceName)
        {
            Title = title;
            Description = description;
            Date = date;
            ServiceName = serviceName;
        }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Date { get; set; } = string.Empty;

        public string ServiceName { get; set; } = string.Empty;
    }
}
