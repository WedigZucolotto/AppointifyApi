using Appointify.Domain;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Commands.Plans.Delete
{
    public class DeletePlanCommandHandler : IRequestHandler<DeletePlanCommand, Nothing>
    {
        private readonly INotificationContext _notification;
        private readonly IPlanRepository _planRepository;
        private readonly ICompanyRepository _companyRepository;

        public DeletePlanCommandHandler(
            IPlanRepository planRepository,
            ICompanyRepository companyRepository,
            INotificationContext notification)
        {
            _notification = notification;
            _planRepository = planRepository;
            _companyRepository = companyRepository;
        }

        public async Task<Nothing> Handle(DeletePlanCommand command, CancellationToken cancellationToken) 
        {
            var plans = await _planRepository.GetAllAsync();

            var plan = plans.FirstOrDefault(plan => plan.Id == command.Id);
            var standardPlan = plans.FirstOrDefault(plan => plan.Name == "Standard");

            if (plan == null || standardPlan == null)
            {
                _notification.AddNotFound("Plano não encontrado.");
                return default;
            }

            var companies = await _companyRepository.GetAllAsync();

            var updatedCompanies = companies
                .Where(company => company.PlanId == command.Id)
                .Select(company => company.SetPlan(standardPlan))
                .ToList();

            _companyRepository.UpdateRange(updatedCompanies);
            _planRepository.Remove(plan);
            
            await _planRepository.UnitOfWork.CommitAsync();

            return Nothing.Value;
        }
    }
}
