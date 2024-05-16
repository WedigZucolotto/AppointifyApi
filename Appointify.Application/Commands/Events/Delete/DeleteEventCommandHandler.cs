using Appointify.Domain;
using Appointify.Domain.Authentication;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Commands.Events.Delete
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Nothing>
    {
        private readonly INotificationContext _notification;
        private readonly IEventRepository _eventRepository;
        private readonly IHttpContext _httpContext;

        public DeleteEventCommandHandler(
            IEventRepository eventRepository, 
            INotificationContext notification,
            IHttpContext httpContext)
        {
            _notification = notification;
            _eventRepository = eventRepository;
            _httpContext = httpContext;
        }

        public async Task<Nothing> Handle(DeleteEventCommand command, CancellationToken cancellationToken) 
        {
            var _event = await _eventRepository.GetByIdAsync(command.Id);

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

            _eventRepository.Remove(_event);
            await _eventRepository.UnitOfWork.CommitAsync();

            return Nothing.Value;
        }
    }
}
