using Appointify.Domain;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Commands.Events.Delete
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Nothing>
    {
        private readonly INotificationContext _notification;
        private readonly IEventRepository _eventRepository;

        public DeleteEventCommandHandler(IEventRepository eventRepository, INotificationContext notification)
        {
            _notification = notification;
            _eventRepository = eventRepository;
        }

        public async Task<Nothing> Handle(DeleteEventCommand command, CancellationToken cancellationToken) 
        {
            var _event = await _eventRepository.GetByIdAsync(command.Id);

            if (_event == null)
            {
                _notification.AddNotFound("Evento não encontrado.");
                return default;
            }

            _eventRepository.Remove(_event);
            await _eventRepository.UnitOfWork.CommitAsync();

            return Nothing.Value;
        }
    }
}
