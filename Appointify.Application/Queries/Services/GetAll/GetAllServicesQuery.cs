using MediatR;

namespace Appointify.Application.Queries.Services.GetAll
{
    public class GetAllServicesQuery : IRequest<IEnumerable<GetAllServicesResponse>>
    {
        public Guid CompanyId { get; set; }
    }
}
