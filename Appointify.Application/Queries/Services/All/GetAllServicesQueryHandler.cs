using Appointify.Domain.Authentication;
using Appointify.Domain.Entities.Enums;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Services.All
{
    public class GetAllServicesQueryHandler : IRequestHandler<GetAllServicesQuery, IEnumerable<GetAllServicesQueryResponse>?>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IHttpContext _httpContext;
        private readonly INotificationContext _notification;

        public GetAllServicesQueryHandler(
            IServiceRepository serviceRepository, 
            IHttpContext httpContext,
            ICompanyRepository companyRepository,
            INotificationContext notification)
        {
            _serviceRepository = serviceRepository;
            _httpContext = httpContext;
            _companyRepository = companyRepository;
            _notification = notification;
        }

        public async Task<IEnumerable<GetAllServicesQueryResponse>?> Handle(GetAllServicesQuery query, CancellationToken cancellationToken)
        {
            var userClaims = _httpContext.GetUserClaims();

            if (query.CompanyId == null && userClaims.Type != UserType.Admin)
            {
                _notification.AddBadRequest("CompanyId deve ser preenchido.");
                return default;
            }

            if (query.CompanyId != null)
            {
                var company = await _companyRepository.GetByIdAsync(query.CompanyId.Value);

                if (company == null)
                {
                    _notification.AddNotFound("Empresa não encontrada.");
                    return default;
                }

                var canEditCompany = company.CanEdit(userClaims);

                if (!canEditCompany)
                {
                    _notification.AddBadRequest("Você não tem permissão para realizar essa operação.");
                    return default;
                }
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
