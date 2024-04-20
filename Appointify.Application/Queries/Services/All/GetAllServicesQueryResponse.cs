namespace Appointify.Application.Queries.Services.All
{
    public class GetAllServicesQueryResponse
    {
        public GetAllServicesQueryResponse(
            Guid id,
            string name,
            string interval)
        {
            Id = id;
            Name = name;
            Interval = interval;
        }

        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Interval { get; set; } = string.Empty;
    }
}
