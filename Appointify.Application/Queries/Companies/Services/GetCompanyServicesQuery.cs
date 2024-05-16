using MediatR;

namespace Appointify.Application.Queries.Companies.Services
{
    public class GetCompanyServicesQuery : IRequest<IEnumerable<GetCompanyServicesQueryResponse>?>
    {
        public GetCompanyServicesQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
