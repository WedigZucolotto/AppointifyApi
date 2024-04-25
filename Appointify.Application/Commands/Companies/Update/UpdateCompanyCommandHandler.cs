using Appointify.Domain;
using Appointify.Domain.Authentication;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Commands.Companies.Update
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Nothing>
    {
        private readonly IPlanRepository _planRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly INotificationContext _notification;
        private readonly IHttpContext _httpContext;

        public UpdateCompanyCommandHandler(
            INotificationContext notification, 
            IPlanRepository planRepository,
            ICompanyRepository companyRepository,
            IUserRepository userRepository,
            IHttpContext httpContext) 
        {
            _notification = notification;
            _planRepository = planRepository;
            _companyRepository = companyRepository;
            _userRepository = userRepository;
            _httpContext = httpContext;
        }

        public async Task<Nothing> Handle(UpdateCompanyCommand command, CancellationToken cancellationToken) 
        {
            var company = await _companyRepository.GetByIdAsync(command.Id);
            
            if (company == null)
            {
                _notification.AddNotFound("Empresa não encontrada.");
                return default;
            }

            var userClaims = _httpContext.GetUserClaims();

            var companyHasUser = company.HasUser(userClaims.Id);

            if (!companyHasUser)
            {
                _notification.AddBadRequest("Usuário não pertence à Empresa.");
                return default;
            }

            var canEditCompany = company.CanEdit(userClaims.Id);

            if (!canEditCompany)
            {
                _notification.AddBadRequest("Você não tem permissão para realizar essa operação.");
                return default;
            }

            if (command.PlanId != null)
            {
                var plan = await _planRepository.GetByIdAsync(command.PlanId.Value);

                if (plan == null)
                {
                    _notification.AddNotFound("Plano não encontrado.");
                    return default;
                }

                company.Plan = plan;
                company.PlanId = plan.Id;
            }

            if (command.Open != null)
            {
                var open = TimeSpan.ParseExact(command.Open, "hh\\:mm", null);
                company.Open = open;
            }

            if (command.Close != null)
            {
                var close = TimeSpan.ParseExact(command.Close, "hh\\:mm", null);
                company.Close = close;
            }

            company.Name = command.Name ?? company.Name;

            _companyRepository.Update(company);
            await _companyRepository.UnitOfWork.CommitAsync();

            return Nothing.Value;
        }
    }
}
