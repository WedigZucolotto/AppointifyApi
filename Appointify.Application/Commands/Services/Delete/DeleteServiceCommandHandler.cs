using Appointify.Domain;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Commands.Services.Delete
{
    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, Nothing>
    {
        private readonly INotificationContext _notification;
        private readonly IServiceRepository _serviceRepository;

        public DeleteServiceCommandHandler(
            INotificationContext notification,
            IServiceRepository serviceRepository) 
        {
            _notification = notification;
            _serviceRepository = serviceRepository;
        }

        public async Task<Nothing> Handle(DeleteServiceCommand command, CancellationToken cancellationToken) 
        {
            var service = await _serviceRepository.GetByIdAsync(command.Id);
            
            if (service == null)
            {
                _notification.AddNotFound("Serviço não encontrado.");
                return default;
            }

            _serviceRepository.Remove(service);
            await _serviceRepository.UnitOfWork.CommitAsync();

            return Nothing.Value;
        }
    }
}
