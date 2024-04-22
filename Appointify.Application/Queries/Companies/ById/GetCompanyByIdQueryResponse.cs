namespace Appointify.Application.Queries.Companies.ById
{
    public class GetCompanyByIdQueryResponse
    {
        public GetCompanyByIdQueryResponse(
            Guid id,
            Guid planId,
            Guid ownerId,
            string name,
            string open,
            string close)
        {
            Id = id;
            Name = name;
            PlanId = planId;
            OwnerId = ownerId;
            Open = open;
            Close = close;
        }

        public Guid Id { get; set; }

        public Guid PlanId { get; set; }

        public Guid OwnerId { get; set; }

        public string Name { get; set; }

        public string Open { get; set; }

        public string Close { get; set; }
    }
}
