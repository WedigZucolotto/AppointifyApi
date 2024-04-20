using MediatR;

namespace Appointify.Application.Queries.Companies.Ids
{
    public class GetAllCompaniesIdsQuery : IRequest<IEnumerable<Guid>>
    {
    }
}
