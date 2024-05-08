using Appointify.Application.Queries.Dtos;
using MediatR;

namespace Appointify.Application.Queries.Services.Options
{
    public class GetServiceOptionsQuery : IRequest<IEnumerable<OptionDto>?>
    {
        public Guid UserId { get; set; }
    }
}
