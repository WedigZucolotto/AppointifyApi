namespace Appointify.Application.Queries.Services.GetAll
{
    public class GetAllServicesResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Interval { get; set; } = string.Empty;

        public GetAllServicesResponse(
            Guid id,
            string name,
            string interval)
        {
            Id = id;
            Name = name;
            Interval = interval;
        }
    }
}
