namespace Appointify.Application.Queries.Companies.Services
{
    public class GetCompanyServicesQueryResponse
    {
        public GetCompanyServicesQueryResponse(
            Guid id,
            string interval,
            string name)
        {
            Id = id;
            Interval = interval;
            Name = name;
        }

        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Interval { get; set; } = string.Empty;
    }
}
