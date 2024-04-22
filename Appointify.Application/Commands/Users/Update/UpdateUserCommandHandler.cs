using Appointify.Domain;
using Appointify.Domain.Authentication;
using Appointify.Domain.Entities.Enums;
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

            if (command.Password != null)
            {
                var hashedPassword = _passwordHasher.Generate(command.Password);
                user.Password = hashedPassword ?? user.Password;
            }

            user.Name = command.Name ?? user.Name;
            user.CompleteName = command.CompleteName ?? user.CompleteName;
            user.Type = command.Type != null ? (UserType)command.Type : user.Type;

            _userRepository.Update(user);
            await _userRepository.UnitOfWork.CommitAsync();

            return Nothing.Value;
        }
    }
}
