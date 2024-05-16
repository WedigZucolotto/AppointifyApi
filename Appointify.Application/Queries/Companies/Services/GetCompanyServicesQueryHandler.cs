using Appointify.Domain.Authentication;
using Appointify.Domain.Entities;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Companies.Services
{
    public class GetCompanyServicesQueryHandler : IRequestHandler<GetCompanyServicesQuery, IEnumerable<GetCompanyServicesQueryResponse>?>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly INotificationContext _notification;
        private readonly IHttpContext _httpContext;

        public GetCompanyServicesQueryHandler(
            ICompanyRepository companyRepository,
            INotificationContext notification,
            IHttpContext httpContext)
        {
            _companyRepository = companyRepository;
            _notification = notification;
            _httpContext = httpContext;
        }

        public async Task<IEnumerable<GetCompanyServicesQueryResponse>?> Handle(GetCompanyServicesQuery query, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(query.Id);

            if (company == null)
            {
                _notification.AddNotFound("Empresa não encontrada.");
                return default;
            }

            var userId = _httpContext.GetUserId();

            if (!company.IsOwner(userId))
            {
                _notification.AddUnauthorized("Você não tem permissão para realizar essa operação.");
                return default;
            }

            return company.Services
                .Select(service => new GetCompanyServicesQueryResponse(
                    service.Id,
                    service.Interval.ToString(@"hh\:mm"), 
                    service.Name));
        }
    }
}
