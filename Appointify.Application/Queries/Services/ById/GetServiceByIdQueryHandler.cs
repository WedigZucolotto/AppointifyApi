using Appointify.Application.Queries.Services.ById;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Services.All
{
    public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, GetServiceByIdQueryResponse?>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly INotificationContext _notification;

        public GetServiceByIdQueryHandler(IServiceRepository serviceRepository, INotificationContext notification)
        {
            _serviceRepository = serviceRepository;
            _notification = notification;
        }

        public async Task<GetServiceByIdQueryResponse?> Handle(GetServiceByIdQuery query, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetByIdAsync(query.Id);

            if (service == null)
            {
                _notification.AddNotFound("Serviço não encontrado.");
                return default;
            }

            return new GetServiceByIdQueryResponse(
                service.Id, 
                service.Name, 
                service.Interval.ToString(@"hh\:mm"));
        }
    }
}
