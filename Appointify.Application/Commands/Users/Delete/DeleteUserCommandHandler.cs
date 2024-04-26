using Appointify.Domain;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Commands.Users.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Nothing>
    {
        private readonly INotificationContext _notification;
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(
            INotificationContext notification,
            IUserRepository userRepository) 
        {
            _notification = notification;
            _userRepository = userRepository;
        }

        public async Task<Nothing> Handle(DeleteUserCommand command, CancellationToken cancellationToken) 
        {
            var user = await _userRepository.GetByIdToDeleteAsync(command.Id);
            
            if (user == null)
            {
                _notification.AddNotFound("Usuário não encontrado.");
                return default;
            }

            _userRepository.Remove(user);
            await _userRepository.UnitOfWork.CommitAsync();

            return Nothing.Value;
        }
    }
}
