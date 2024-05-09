using Appointify.Domain;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Commands.Plans.Update
{
    public class UpdatePlanCommandHandler : IRequestHandler<UpdatePlanCommand, Nothing>
    {
        private readonly IPlanRepository _planRepository;
        private readonly INotificationContext _notification;

        public UpdatePlanCommandHandler(IPlanRepository planRepository, INotificationContext notification) 
        {
            _planRepository = planRepository;
            _notification = notification;
        }

        public async Task<Nothing> Handle(UpdatePlanCommand command, CancellationToken cancellationToken) 
        {
            var plan = await _planRepository.GetByIdAsync(command.Id);
            
            if (plan == null)
            {
                _notification.AddNotFound("Plano não encontrado.");
                return default;
            }

            if (!string.IsNullOrEmpty(command.Name))
            {
                plan.Name = command.Name;
            }

            if (command.ShowExtraFields != null)
            {
                plan.ShowExtraFields = command.ShowExtraFields.Value;
            }

            _planRepository.Update(plan);
            await _planRepository.UnitOfWork.CommitAsync();

            return Nothing.Value;
        }
    }
}
