using Appointify.Domain.Authentication;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Events.ById
{
    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, GetEventByIdQueryResponse?>
    {
        private readonly IEventRepository _eventRepository;
        private readonly INotificationContext _notification;
        private readonly IHttpContext _httpContext;

        public GetEventByIdQueryHandler(
            IEventRepository eventRepository,
            INotificationContext notification, 
            IHttpContext httpContext)
        {
            _eventRepository = eventRepository;
            _notification = notification;
            _httpContext = httpContext;
        }

        public async Task<GetEventByIdQueryResponse?> Handle(GetEventByIdQuery query, CancellationToken cancellationToken)
        {
            var _event = await _eventRepository.GetByIdAsync(query.Id);

            if (_event == null)
            {
                _notification.AddNotFound("Evento não encontrado.");
                return default;
            }

            var userId = _httpContext.GetUserId();

            if (_event.UserId != userId)
            {
                _notification.AddUnauthorized("Você não tem permissão para realizar essa operação.");
                return default;
            }

            return new GetEventByIdQueryResponse(
                _event.Id,
                _event.Title,
                _event.Date.ToShortDateString(), 
                _event.Service.Name);
        }
    }
}
