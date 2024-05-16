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
        private readonly INotificationContext _notification;
        private readonly IHttpContext _httpContext;

        public UpdateCompanyCommandHandler(
            INotificationContext notification, 
            IPlanRepository planRepository,
            ICompanyRepository companyRepository,
            IHttpContext httpContext) 
        {
            _notification = notification;
            _planRepository = planRepository;
            _companyRepository = companyRepository;
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

            var userId = _httpContext.GetUserId();

            if (!company.IsOwner(userId))
            {
                _notification.AddUnauthorized("Você não tem permissão para realizar essa operação.");
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

            if (!string.IsNullOrEmpty(command.Open))
            {
                var open = TimeSpan.ParseExact(command.Open, "hh\\:mm", null);
                company.Open = open;
            }

            if (!string.IsNullOrEmpty(command.Close))
            {
                var close = TimeSpan.ParseExact(command.Close, "hh\\:mm", null);
                company.Close = close;
            }

            if (!string.IsNullOrEmpty(command.Name))
            {
                company.Name = command.Name;
            }

            _companyRepository.Update(company);
            await _companyRepository.UnitOfWork.CommitAsync();

            return Nothing.Value;
        }
    }
}
