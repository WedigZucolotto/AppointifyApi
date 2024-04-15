using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Services.GetAll
{
    public class GetAllServicesHandler : IRequestHandler<GetAllServicesQuery, IEnumerable<GetAllServicesResponse>>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetAllServicesHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<IEnumerable<GetAllServicesResponse>> Handle(GetAllServicesQuery query, CancellationToken cancellationToken)
        {
            var services = await _serviceRepository
                .GetAllFilteredByCompanyAsync(query.CompanyId);

            return services.Select(
                service => new GetAllServicesResponse(
                    service.Id,
                    service.Name,
                    service.Interval.ToString(@"hh\:mm")));
        }
    }
}
