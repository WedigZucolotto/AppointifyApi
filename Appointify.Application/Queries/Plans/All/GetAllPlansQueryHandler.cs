using Appointify.Application.Queries.Plans.All;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Services.All
{
    public class GetAllPlansQueryHandler : IRequestHandler<GetAllPlansQuery, IEnumerable<GetAllPlansQueryResponse>>
    {
        private readonly IPlanRepository _planRepository;

        public GetAllPlansQueryHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<IEnumerable<GetAllPlansQueryResponse>> Handle(GetAllPlansQuery query, CancellationToken cancellationToken)
        {
            var plans = await _planRepository.GetAllAsync();

            return plans.Select(
                plan => new GetAllPlansQueryResponse(plan.Id, plan.Name, plan.ShowExtraFields));
        }
    }
}
