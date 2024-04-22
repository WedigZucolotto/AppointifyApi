using Appointify.Domain;
using Appointify.Domain.Authentication;
using Appointify.Domain.Entities;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Appointify.Application.Commands.Events.Create
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Nothing>
    {
        private readonly IHttpContext _httpContext;
        private readonly INotificationContext _notification;
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IEventRepository _eventRepository;

        public CreateEventCommandHandler(
            IHttpContext httpContext,
            INotificationContext notification, 
            IUserRepository userRepository,
            ICompanyRepository companyRepository,
            IServiceRepository serviceRepository,
            IEventRepository eventRepository)
        {
            _httpContext = httpContext;
            _notification = notification;
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _serviceRepository = serviceRepository;
            _eventRepository = eventRepository;
        }

        public async Task<Nothing> Handle(CreateEventCommand command, CancellationToken cancellationToken) 
        {
            // se UserId vier do token -> Funcionário criou evento
            // se UserId vier da request -> Cliente escolheu Funcionário
            // se UserId não vier -> Escolher usuário aleátório

            var service = await _serviceRepository.GetByIdAsync(command.ServiceId);

            if (service == null)
            {
                _notification.AddNotFound("Serviço não encontrado.");
                return default;
            }

            var userId = _httpContext.GetUserClaims().Id ?? command.UserId;
            var user = await _userRepository.GetByIdAsync(userId ?? Guid.Empty);

            var description = $"Marcado por: {command.Name} às {new DateTime()}\nContato: {command.Contact}";
            var date = DateTime.Parse(command.Date);

            if (user == null && userId != null) 
            {
                _notification.AddNotFound("Usuário não encontrado.");
                return default;
            }

            if (userId == null)
            {
                if (command.CompanyId == null)
                {
                    _notification.AddBadRequest("Id da empresa não pode ser nulo.");
                    return default;
                }

                var company = await _companyRepository.GetByIdAsync(command.CompanyId.Value);

                if (company == null)
                {
                    _notification.AddNotFound("Empresa não encontrada.");
                    return default;
                }

                user = company.Users.FirstOrDefault(u => u.IsAvailable(date, service.Interval));

                if (user == null)
                {
                    _notification.AddBadRequest("Todos usuários estão ocupados.");
                    return default;
                }
            }
            
            var userAvailable = user.IsAvailable(date, service.Interval);

            if (!userAvailable)
            {
                _notification.AddBadRequest("Usuário está ocupado.");
                return default;
            }

            var _event = new Event(command.Name, description, date, user, service);

            _eventRepository.Add(_event);
            await _eventRepository.UnitOfWork.CommitAsync();

            return Nothing.Value;
        }
    }
}
