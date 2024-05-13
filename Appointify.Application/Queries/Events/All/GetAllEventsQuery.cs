using MediatR;

namespace Appointify.Application.Queries.Events.All
{
    public class GetAllEventsQuery : IRequest<IEnumerable<GetAllEventsQueryResponse>>
    {
        public string? Title { get; set; }

        public string? Date { get; set; }

        public string? ServiceName { get; set; }

        public Guid UserId { get; set; }

        public Guid CompanyId { get; set; }
    }
}
