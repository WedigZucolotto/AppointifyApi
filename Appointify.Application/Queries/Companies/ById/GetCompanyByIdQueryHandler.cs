using Appointify.Domain.Authentication;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Companies.ById
{
    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, GetCompanyByIdQueryResponse?>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly INotificationContext _notification;
        private readonly IHttpContext _httpContext;

        public GetCompanyByIdQueryHandler(
            ICompanyRepository companyRepository,
            INotificationContext notification, 
            IHttpContext httpContext)
        {
            _companyRepository = companyRepository;
            _notification = notification;
            _httpContext = httpContext;
        }

        public async Task<GetCompanyByIdQueryResponse?> Handle(GetCompanyByIdQuery query, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(query.Id);

            if (company == null)
            {
                _notification.AddNotFound("Empresa não encontrada.");
                return default;
            }

            var userClaims = _httpContext.GetUserClaims();

            var canEditCompany = company.CanEdit(userClaims);

            if (!canEditCompany)
            {
                _notification.AddBadRequest("Você não tem permissão para realizar essa operação.");
                return default;
            }

            return new GetCompanyByIdQueryResponse(
                company.Id,
                company.PlanId,
                company.Name,
                company.Open.ToString(@"hh\:mm"),
                company.Close.ToString(@"hh\:mm"));
        }
    }
}
