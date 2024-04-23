using Appointify.Domain.Authentication;
using Appointify.Domain.Entities;
using Appointify.Domain.Entities.Enums;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Services.All
{
    public class GetAllServicesQueryHandler : IRequestHandler<GetAllServicesQuery, IEnumerable<GetAllServicesQueryResponse>>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContext _httpContext;
        private readonly INotificationContext _notification;

        public GetAllServicesQueryHandler(
            IServiceRepository serviceRepository, 
            IHttpContext httpContext,
            IUserRepository userRepository,
            INotificationContext notification)
        {
            _serviceRepository = serviceRepository;
            _httpContext = httpContext;
            _userRepository = userRepository;
            _notification = notification;
        }

        public async Task<IEnumerable<GetAllServicesQueryResponse>> Handle(GetAllServicesQuery query, CancellationToken cancellationToken)
        {
            var userClaims = _httpContext.GetUserClaims();

            if (query.CompanyId == null && userClaims.Type != UserType.Admin)
            {
                _notification.AddBadRequest("CompanyId deve ser preenchido.");
                return default;
            }

            var isUserCompany = await _userRepository
                .VerifyCompanyAsync(userClaims.Id ?? Guid.Empty, query.CompanyId ?? Guid.Empty);

            if (!isUserCompany)
            {
                _notification.AddBadRequest("Usuário não pertence à Empresa.");
                return default;
            }

            var services = await _serviceRepository.GetFilteredAsync(query.CompanyId, query.Name);

            return services.Select(
                service => new GetAllServicesQueryResponse(
                    service.Id,
                    service.Name,
                    service.Company.Name,
                    service.Interval.ToString(@"hh\:mm")));
        }
    }
}
