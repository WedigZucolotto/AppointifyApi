using Appointify.Domain.Authentication;
using Appointify.Domain.Entities.Enums;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Users.All
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<GetAllUsersQueryResponse>?>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IHttpContext _httpContext;
        private readonly INotificationContext _notification;

        public GetAllUsersQueryHandler(
            IUserRepository userRepository, 
            IHttpContext httpContext,
            INotificationContext notification,
            ICompanyRepository companyRepository)
        {
            _userRepository = userRepository;
            _httpContext = httpContext;
            _notification = notification;
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<GetAllUsersQueryResponse>?> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
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
                    _notification.AddUnauthorized("Você não tem permissão para realizar essa operação.");
                    return default;
                }
            }

            var users = await _userRepository
                .GetFilteredAsync(query.CompanyId, query.Name, query.CompleteName, query.Type);

            return users.Select(
                user => new GetAllUsersQueryResponse(
                    user.Id,
                    user.Name,
                    user.CompleteName,
                    user.Type.ToString(),
                    user.Company.Name));
        }
    }
}
