using Appointify.Application.Queries.Dtos;
using MediatR;

namespace Appointify.Application.Queries.Plans.Options
{
    public class GetPlanOptionsQuery : IRequest<IEnumerable<OptionDto>>
    {
    }
}
