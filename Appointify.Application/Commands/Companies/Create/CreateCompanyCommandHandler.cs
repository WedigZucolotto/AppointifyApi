using Appointify.Domain;
using Appointify.Domain.Entities;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Commands.Companies.Create
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Nothing>
    {
        private readonly INotificationContext _notification;
        private readonly IPlanRepository _planRepository;
        private readonly ICompanyRepository _companyRepository;

        public CreateCompanyCommandHandler(
            INotificationContext notification, 
            IPlanRepository planRepository,
            ICompanyRepository companyRepository) 
        {
            _notification = notification;
            _planRepository = planRepository;
            _companyRepository = companyRepository;
        }

        public async Task<Nothing> Handle(CreateCompanyCommand command, CancellationToken cancellationToken) 
        {
            var plan = await _planRepository.GetByIdAsync(command.PlanId);

            if (plan == null)
            {
                _notification.AddNotFound("Plano não encontrado.");
                return default;
            }

            var open = TimeSpan.ParseExact(command.Open, "hh\\:mm", null);
            var close = TimeSpan.ParseExact(command.Close, "hh\\:mm", null);

            var company = new Company(command.Name, plan, open, close);

            _companyRepository.Add(company);
            await _companyRepository.UnitOfWork.CommitAsync();

            return Nothing.Value;
        }
    }
}
