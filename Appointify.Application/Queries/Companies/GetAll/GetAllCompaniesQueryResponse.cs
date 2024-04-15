namespace Appointify.Application.Queries.Companies.GetAll
{
    public class GetAllCompaniesResponse
    {
        public GetAllCompaniesResponse(
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
