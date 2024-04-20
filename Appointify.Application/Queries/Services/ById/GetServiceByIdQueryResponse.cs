namespace Appointify.Application.Queries.Services.ById
{
    public class GetServiceByIdQueryResponse
    {
        public GetServiceByIdQueryResponse(
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
