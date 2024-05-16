using Appointify.Domain.Authentication;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Users.ById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdQueryResponse?>
    {
        private readonly IUserRepository _userRepository;
        private readonly INotificationContext _notification;
        private readonly IHttpContext _httpContext;

        public GetUserByIdQueryHandler(
            IUserRepository userRepository, 
            INotificationContext notification,
            IHttpContext httpContext)
        {
            _userRepository = userRepository;
            _notification = notification;
            _httpContext = httpContext;
        }

        public async Task<GetUserByIdQueryResponse?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(query.Id);
            var userId = _httpContext.GetUserId();

            if (user == null || query.Id != userId)
            {
                _notification.AddNotFound("Usuário não encontrado.");
                return default;
            }

            return new GetUserByIdQueryResponse(user.Id, user.Name, user.CompleteName);
        }
    }
}
