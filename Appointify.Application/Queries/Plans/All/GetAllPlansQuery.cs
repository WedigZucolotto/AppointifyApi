using MediatR;

namespace Appointify.Application.Queries.Plans.All
{
    public class GetAllPlansQuery : IRequest<IEnumerable<GetAllPlansQueryResponse>>
    {
    }
}
