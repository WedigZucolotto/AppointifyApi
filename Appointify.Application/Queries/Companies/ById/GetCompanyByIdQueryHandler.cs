using Appointify.Domain.Authentication;
using Appointify.Domain.Entities.Enums;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Companies.ById
{
    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, GetCompanyByIdQueryResponse?>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly INotificationContext _notification;
        private readonly IHttpContext _httpContext;

        public GetCompanyByIdQueryHandler(
            ICompanyRepository companyRepository,
            IUserRepository userRepository,
            INotificationContext notification, 
            IHttpContext httpContext)
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
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

            var isUserCompany = await _userRepository
                .VerifyCompanyAsync(userClaims.Id ?? Guid.Empty, company.Id);

            if (!isUserCompany)
            {
                _notification.AddBadRequest("Usuário não pertence à Empresa.");
                return default;
            }

            var isOwnerUser = company.Users
                .Where(u => u.Type == UserType.Owner)
                .Any(u => u.Id == userClaims.Id);

            var isAdminUser = userClaims.Type == UserType.Admin;

            if (!isOwnerUser && !isAdminUser)
            {
                _notification.AddBadRequest("Usuário não é o proprietário.");
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
