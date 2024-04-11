using MediatR;

namespace Appointify.Application.Queries.Events.Day
{
    public class GetDayEventsQuery : IRequest<IEnumerable<GetDayEventsQueryResponse>>
    {
    }
}
