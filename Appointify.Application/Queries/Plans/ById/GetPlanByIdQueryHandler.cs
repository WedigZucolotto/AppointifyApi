using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Plans.ById
{
    public class GetPlanByIdQueryHandler : IRequestHandler<GetPlanByIdQuery, GetPlanByIdQueryResponse?>
    {
        private readonly IPlanRepository _planRepository;
        private readonly INotificationContext _notification;

        public GetPlanByIdQueryHandler(IPlanRepository planRepository, INotificationContext notification)
        {
            _planRepository = planRepository;
            _notification = notification;
        }

        public async Task<GetPlanByIdQueryResponse?> Handle(GetPlanByIdQuery query, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetByIdAsync(query.Id);

            if (plan == null)
            {
                _notification.AddNotFound("Plano não encontrado.");
                return default;
            }

            return new GetPlanByIdQueryResponse(plan.Id, plan.Name, plan.ShowExtraFields);
        }
    }
}
