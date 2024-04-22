using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Users.All
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<GetAllUsersQueryResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<GetAllUsersQueryResponse>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(
                user => new GetAllUsersQueryResponse(
                    user.Name,
                    user.CompleteName,
                    user.Type.ToString(),
                    user.Company.Name));
        }
    }
}
