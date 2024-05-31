using Appointify.Domain;
using Appointify.Domain.Authentication;
using Appointify.Domain.Entities;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;
using System.Globalization;

namespace Appointify.Application.Commands.Events.Create
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Nothing>
    {
        private readonly IHttpContext _httpContext;
        private readonly INotificationContext _notification;
        private readonly IUserRepository _userRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IEventRepository _eventRepository;

        public CreateEventCommandHandler(
            IHttpContext httpContext,
            INotificationContext notification, 
            IUserRepository userRepository,
            IServiceRepository serviceRepository,
            IEventRepository eventRepository)
        {
            _httpContext = httpContext;
            _notification = notification;
            _userRepository = userRepository;
            _serviceRepository = serviceRepository;
            _eventRepository = eventRepository;
        }

        public async Task<Nothing> Handle(CreateEventCommand command, CancellationToken cancellationToken) 
        {
            var culture = new CultureInfo("pt-BR");
            var date = DateTime.Parse(command.Date, culture);
            var SAMTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            var today = TimeZoneInfo.ConvertTime(DateTime.Today, SAMTimeZone);

            if (date < today)
            {
                _notification.AddBadRequest("Horário marcado deve ser no futuro.");
                return default;
            }

            var service = await _serviceRepository.GetByIdAsync(command.ServiceId);

            if (service == null)
            {
                _notification.AddNotFound("Serviço não encontrado.");
                return default;
            }

            var userId = _httpContext.GetUserId() ?? command.UserId;
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null) 
            {
                _notification.AddNotFound("Usuário não encontrado.");
                return default;
            }

            if (!user.IsAvailable(date, service.Interval))
            {
                _notification.AddBadRequest("Usuário está ocupado.");
                return default;
            }

            var _event = new Event(command.Name, date, user, service);

            _eventRepository.Add(_event);
            await _eventRepository.UnitOfWork.CommitAsync();

            return Nothing.Value;
        }
    }
}
