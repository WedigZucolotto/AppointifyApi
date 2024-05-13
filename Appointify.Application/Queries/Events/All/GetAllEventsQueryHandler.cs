using Appointify.Domain.Repositories;
using MediatR;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            var culture = new CultureInfo("pt-BR");
            DateTime? date = null;

            if (query.Date != null)
            {
                date = DateTime.Parse(query.Date, culture);
            }

            var events = await _eventRepository.GetFilteredAsync(
                query.Title,
                date, 
                query.ServiceName,
                query.UserId,
                query.CompanyId);

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
