using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Services.All
{
    public class GetAllServicesQueryHandler : IRequestHandler<GetAllServicesQuery, IEnumerable<GetAllServicesQueryResponse>>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetAllServicesQueryHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<IEnumerable<GetAllServicesQueryResponse>> Handle(GetAllServicesQuery query, CancellationToken cancellationToken)
        {
            var services = await _serviceRepository
                .GetAllFilteredByCompanyAsync(query.CompanyId);

            return services.Select(
                service => new GetAllServicesQueryResponse(
                    service.Id,
                    service.Name,
                    service.Interval.ToString(@"hh\:mm")));
        }
    }
}
