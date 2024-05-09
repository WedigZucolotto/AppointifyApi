using Appointify.Domain;
using Appointify.Domain.Entities;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Commands.Plans.Create
{
    public class CreatePlanCommandHandler : IRequestHandler<CreatePlanCommand, Nothing>
    {
        private readonly IPlanRepository _planRepository;

        public CreatePlanCommandHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<Nothing> Handle(CreatePlanCommand command, CancellationToken cancellationToken) 
        {
            var plan = new Plan(command.Name, command.ShowExtraFields);

            _planRepository.Add(plan);
            await _planRepository.UnitOfWork.CommitAsync();

            return Nothing.Value;
        }
    }
}
