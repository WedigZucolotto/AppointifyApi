using MediatR;

namespace Appointify.Application.Queries.Services.ById
{
    public class GetServiceByIdQuery : IRequest<GetServiceByIdQueryResponse?>
    {
        public GetServiceByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
