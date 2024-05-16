using MediatR;

namespace Appointify.Application.Queries.Events.ById
{
    public class GetEventByIdQuery : IRequest<GetEventByIdQueryResponse?>
    {
        public GetEventByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
