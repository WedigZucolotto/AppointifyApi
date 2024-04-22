using MediatR;

namespace Appointify.Application.Commands.Users.Login
{
    public class LoginUserCommand : IRequest<LoginUserCommandResponse?>
    {
        public string Name { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
