using Appointify.Domain;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Commands.Services.Update
{
    public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, Nothing>
    {
        private readonly INotificationContext _notification;
        private readonly IServiceRepository _serviceRepository;

        public UpdateServiceCommandHandler(
            INotificationContext notification,
            IServiceRepository serviceRepository) 
        {
            _notification = notification;
            _serviceRepository = serviceRepository;
        }

        public async Task<Nothing> Handle(UpdateServiceCommand command, CancellationToken cancellationToken) 
        {
            var service = await _serviceRepository.GetByIdAsync(command.Id);
            
            if (service == null)
            {
                _notification.AddNotFound("Serviço não encontrado.");
                return default;
            }

            if (command.Interval != null)
            {
                var interval = TimeSpan.ParseExact(command.Interval, "hh\\:mm", null);
                service.Interval = interval;
            }

            service.Name = command.Name ?? service.Name;

            _serviceRepository.Update(service);
            await _serviceRepository.UnitOfWork.CommitAsync();

            return Nothing.Value;
        }
    }
}
