using MediatR;

namespace Appointify.Application.Queries.Companies.All
{
    public class GetAllCompaniesQuery : IRequest<IEnumerable<GetAllCompaniesQueryResponse>>
    {
    }
}
