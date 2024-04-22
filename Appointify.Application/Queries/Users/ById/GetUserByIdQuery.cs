using MediatR;

namespace Appointify.Application.Queries.Users.ById
{
    public class GetUserByIdQuery : IRequest<GetUserByIdQueryResponse?>
    {
        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
