using MediatR;

namespace Appointify.Application.Queries.Services.All
{
    public class GetAllServicesQuery : IRequest<IEnumerable<GetAllServicesQueryResponse>>
    {
        public Guid? CompanyId { get; set; }

        public string? Name { get; set; } = string.Empty;
    }
}
