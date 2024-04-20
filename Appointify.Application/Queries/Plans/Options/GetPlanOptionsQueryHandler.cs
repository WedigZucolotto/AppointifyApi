using Appointify.Application.Queries.Dtos;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Plans.Options
{
    public class GetPlanOptionsQueryHandler : IRequestHandler<GetPlanOptionsQuery, IEnumerable<OptionDto>>
    {
        private readonly IPlanRepository _planRepository;

        public GetPlanOptionsQueryHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<IEnumerable<OptionDto>> Handle(GetPlanOptionsQuery query, CancellationToken cancellationToken)
        {
            var services = await _planRepository.GetAllAsync();
            return services.Select(s => new OptionDto(s.Name, s.Id));
        }
    }
}
