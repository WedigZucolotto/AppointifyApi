using Appointify.Domain;
using Appointify.Domain.Authentication;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Commands.Users.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Nothing>
    {
        private readonly INotificationContext _notification;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UpdateUserCommandHandler(
            INotificationContext notification,
            IUserRepository userRepository,
            IPasswordHasher passwordHasher) 
        {
            _notification = notification;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Nothing> Handle(UpdateUserCommand command, CancellationToken cancellationToken) 
        {
            var user = await _userRepository.GetByIdAsync(command.Id);
            
            if (user == null)
            {
                _notification.AddNotFound("Usuário não encontrado.");
                return default;
            }

            if (!string.IsNullOrEmpty(command.Password))
            {
                var hashedPassword = _passwordHasher.Generate(command.Password);
                user.Password = hashedPassword ?? user.Password;
            }

            if (!string.IsNullOrEmpty(command.Name))
            {
                user.Name = command.Name;
            }

            if (!string.IsNullOrEmpty(command.CompleteName))
            {
                user.CompleteName = command.CompleteName;
            }

            if (command.IsOwner != null)
            {
                user.SetType(command.IsOwner.Value);
            }

            _userRepository.Update(user);
            await _userRepository.UnitOfWork.CommitAsync();

            return Nothing.Value;
        }
    }
}
