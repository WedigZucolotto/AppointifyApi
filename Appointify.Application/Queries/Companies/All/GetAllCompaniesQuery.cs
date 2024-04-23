using MediatR;

namespace Appointify.Application.Queries.Companies.All
{
    public class GetAllCompaniesQuery : IRequest<IEnumerable<GetAllCompaniesQueryResponse>>
    {
        public string? Name { get; set; }

        public Guid? PlanId { get; set; }
    }
}
