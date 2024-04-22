using FluentValidation;

namespace Appointify.Application.Commands.Users.Login
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(l => l.Name)
                .NotEmpty().WithMessage("Propriedade obrigatória: Name");

            RuleFor(l => l.Password)
                .NotEmpty().WithMessage("Propriedade obrigatória: Password"); ;
        }
    }
}
