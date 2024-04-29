using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Users.ById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdQueryResponse?>
    {
        private readonly IUserRepository _userRepository;
        private readonly INotificationContext _notification;

        public GetUserByIdQueryHandler(IUserRepository userRepository, INotificationContext notification)
        {
            _userRepository = userRepository;
            _notification = notification;
        }

        public async Task<GetUserByIdQueryResponse?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(query.Id);

            if (user == null)
            {
                _notification.AddNotFound("Usuário não encontrado.");
                return default;
            }

            return new GetUserByIdQueryResponse(
                user.Id,
                user.Name, 
                user.CompleteName, 
                user.Type.ToString(), 
                user.CompanyId);
        }
    }
}
