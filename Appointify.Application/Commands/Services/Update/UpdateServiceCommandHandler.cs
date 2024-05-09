using Appointify.Domain;
using Appointify.Domain.Authentication;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Commands.Services.Update
{
    public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, Nothing>
    {
        private readonly INotificationContext _notification;
        private readonly IServiceRepository _serviceRepository;
        private readonly IHttpContext _httpContext;

        public UpdateServiceCommandHandler(
            INotificationContext notification,
            IServiceRepository serviceRepository,
            IHttpContext httpContext) 
        {
            _notification = notification;
            _serviceRepository = serviceRepository;
            _httpContext = httpContext;
        }

        public async Task<Nothing> Handle(UpdateServiceCommand command, CancellationToken cancellationToken) 
        {
            var service = await _serviceRepository.GetByIdAsync(command.Id);
            
            if (service == null)
            {
                _notification.AddNotFound("Serviço não encontrado.");
                return default;
            }

            var userClaims = _httpContext.GetUserClaims();

            var canEditService = service.CanEdit(userClaims);

            if (!canEditService)
            {
                _notification.AddUnauthorized("Você não tem permissão para realizar essa operação.");
                return default;
            }

            if (!string.IsNullOrEmpty(command.Interval))
            {
                var interval = TimeSpan.ParseExact(command.Interval, "hh\\:mm", null);
                service.Interval = interval;
            }

            if (!string.IsNullOrEmpty(command.Name))
            {
                service.Name = command.Name;
            }

            _serviceRepository.Update(service);
            await _serviceRepository.UnitOfWork.CommitAsync();

            return Nothing.Value;
        }
    }
}
