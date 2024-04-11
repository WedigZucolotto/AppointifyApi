using MediatR;

namespace Appointify.Application.Queries.Events.Day
{
    public class GetDayEventsQueryHandler : IRequestHandler<GetDayEventsQuery, IEnumerable<GetDayEventsQueryResponse>>
    {
        public GetDayEventsQueryHandler() { }

        public async Task<IEnumerable<GetDayEventsQueryResponse>> Handle(GetDayEventsQuery query, CancellationToken cancellationToken)
        {
            return new List<GetDayEventsQueryResponse>() {
                new GetDayEventsQueryResponse() { Title = "eai" } };
        }
    }
}
