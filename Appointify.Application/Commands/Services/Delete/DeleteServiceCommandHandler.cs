using Appointify.Domain;
using Appointify.Domain.Authentication;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Commands.Services.Delete
{
    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, Nothing>
    {
        private readonly INotificationContext _notification;
        private readonly IServiceRepository _serviceRepository;
        private readonly IHttpContext _httpContext;

        public DeleteServiceCommandHandler(
            INotificationContext notification,
            IServiceRepository serviceRepository,
            IHttpContext httpContext) 
        {
            _notification = notification;
            _serviceRepository = serviceRepository;
            _httpContext = httpContext;
        }

        public async Task<Nothing> Handle(DeleteServiceCommand command, CancellationToken cancellationToken) 
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
                _notification.AddBadRequest("Você não tem permissão para realizar essa operação.");
                return default;
            }

            _serviceRepository.Remove(service);
            await _serviceRepository.UnitOfWork.CommitAsync();

            return Nothing.Value;
        }
    }
}
