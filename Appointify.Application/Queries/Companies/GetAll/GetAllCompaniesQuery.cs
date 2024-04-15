using MediatR;

namespace Appointify.Application.Queries.Companies.GetAll
{
    public class GetAllCompaniesQuery : IRequest<IEnumerable<GetAllCompaniesResponse>>
    {
    }
}
