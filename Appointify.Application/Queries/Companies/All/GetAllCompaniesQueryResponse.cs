namespace Appointify.Application.Queries.Companies.All
{
    public class GetAllCompaniesQueryResponse
    {
        public GetAllCompaniesQueryResponse(
            Guid id,
            string name,
            string planName,
            string open,
            string close)
        {
            Id = id;
            Name = name;
            PlanName = planName;
            Open = open;
            Close = close;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string PlanName { get; set; }

        public string Open { get; set; }

        public string Close { get; set; }
    }
}
