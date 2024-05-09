using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Events.All
{
    public class GetAllEventsQueryHandler : IRequestHandler<GetAllEventsQuery, IEnumerable<GetAllEventsQueryResponse>>
    {
        private readonly IEventRepository _eventRepository;

        public GetAllEventsQueryHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<GetAllEventsQueryResponse>> Handle(GetAllEventsQuery query, CancellationToken cancellationToken)
        {
            var events = await _eventRepository.GetFilteredAsync(query.Title, query.Date, query.ServiceName);

            return events.Select(
                _event => new GetAllEventsQueryResponse(
                    _event.Id, 
                    _event.Title,
                    _event.Description,
                    _event.Date.ToString("dd/MM/yyyy"),
                    _event.Service.Name));
        }
    }
}
