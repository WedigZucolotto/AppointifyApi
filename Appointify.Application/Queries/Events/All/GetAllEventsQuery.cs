using MediatR;

namespace Appointify.Application.Queries.Events.All
{
    public class GetAllEventsQuery : IRequest<IEnumerable<GetAllEventsQueryResponse>>
    {
        public string? Title { get; set; }

        public DateTime? Date { get; set; }

        public string? ServiceName { get; set; }
    }
}
