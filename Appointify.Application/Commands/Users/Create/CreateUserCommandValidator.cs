using FluentValidation;

namespace Appointify.Application.Commands.Users.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty().WithMessage("Propriedade obrigatória: Name");

            RuleFor(u => u.CompleteName)
                .NotEmpty().WithMessage("Propriedade obrigatória: CompleteName");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Propriedade obrigatória: Password")
                .MinimumLength(8).WithMessage("Senha deve ter no mínimo 8 caracteres.")
                .Matches("[0-9]").WithMessage("Senha deve ter pelo menos um número.")
                .Matches("[!@#$%^&*(),.?\":{}|<>]").WithMessage("Senha deve ter pelo menos um caracter especial.")
                .Matches("[A-Z]").WithMessage("Senha deve ter pelo menos uma letra maiúscula.");

            RuleFor(u => u.Type)
                .NotNull().WithMessage("Propriedade obrigatória: Type");

            RuleFor(u => u.CompanyId)
                .NotNull().WithMessage("Propriedade obrigatória: CompanyId");
        }
    }
}
