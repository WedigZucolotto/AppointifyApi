using Appointify.Domain.Authentication;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Commands.Users.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserCommandResponse?>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHasher _passwordHasher;
        private readonly INotificationContext _notification;

        public LoginUserCommandHandler(
            IUserRepository userRepository,
            IJwtProvider jwtProvider,
            IPasswordHasher passwordHasher,
            INotificationContext notification)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _passwordHasher = passwordHasher;
            _notification = notification;
        }

        public async Task<LoginUserCommandResponse?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByNameAsync(request.Name);

            if (user == null)
            {
                _notification.AddUnauthorized("Credenciais inválidas.");
                return default;
            }

            var validPassword = _passwordHasher.Verify(user.Password, request.Password);

            if (!validPassword)
            {
                _notification.AddUnauthorized("Credenciais inválidas.");
                return default;
            }

            var token = _jwtProvider.Generate(user);

            return new LoginUserCommandResponse(
                token, 
                user.CompleteName,
                user.IsOwner,
                user.CompanyId, 
                user.Id);
        }
    }
}
