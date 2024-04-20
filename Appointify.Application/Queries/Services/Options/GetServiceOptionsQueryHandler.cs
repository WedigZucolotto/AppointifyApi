using Appointify.Application.Queries.Dtos;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Services.Options
{
    public class GetServiceOptionsQueryHandler : IRequestHandler<GetServiceOptionsQuery, IEnumerable<OptionDto>>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetServiceOptionsQueryHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<IEnumerable<OptionDto>> Handle(GetServiceOptionsQuery query, CancellationToken cancellationToken)
        {
            var services = await _serviceRepository.GetAllAsync();
            return services.Select(s => new OptionDto(s.Name, s.Id));
        }
    }
}
