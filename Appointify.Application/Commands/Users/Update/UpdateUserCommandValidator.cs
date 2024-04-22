using FluentValidation;

namespace Appointify.Application.Commands.Users.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator() 
        {
            RuleFor(u => u.Id)
                .NotEmpty().WithMessage("Propriedade obrigatória: Id");

            RuleFor(u => u.Password)
                .MinimumLength(8).WithMessage("Senha deve ter no mínimo 8 caracteres.")
                .Matches("[0-9]").WithMessage("Senha deve ter pelo menos um número.")
                .Matches("[!@#$%^&*(),.?\":{}|<>]").WithMessage("Senha deve ter pelo menos um caracter especial.")
                .Matches("[A-Z]").WithMessage("Senha deve ter pelo menos uma letra maiúscula.");
        }
    }
}
