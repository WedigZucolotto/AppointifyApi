using MediatR;

namespace Appointify.Application.Queries.Users.All
{
    public class GetAllUsersQuery : IRequest<IEnumerable<GetAllUsersQueryResponse>>
    {
    }
}
