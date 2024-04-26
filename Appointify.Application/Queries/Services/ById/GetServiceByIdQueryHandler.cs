using Appointify.Domain.Authentication;
using Appointify.Domain.Entities;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Services.ById
{
    public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, GetServiceByIdQueryResponse?>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly INotificationContext _notification;
        private readonly IHttpContext _httpContext;

        public GetServiceByIdQueryHandler(
            IServiceRepository serviceRepository,
            INotificationContext notification,
            IHttpContext httpContext)
        {
            _serviceRepository = serviceRepository;
            _notification = notification;
            _httpContext = httpContext;
        }

        public async Task<GetServiceByIdQueryResponse?> Handle(GetServiceByIdQuery query, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetByIdAsync(query.Id);

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

            return new GetServiceByIdQueryResponse(
                service.Id,
                service.Name,
                service.Interval.ToString(@"hh\:mm"));
        }
    }
}
