using FluentValidation;

namespace Appointify.Application.Commands.Users.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator() 
        {
            RuleFor(u => u.Id)
                .NotEmpty().WithMessage("Propriedade obrigatória: Id");

            When(u => !string.IsNullOrEmpty(u.Name), () =>
            {
                RuleFor(u => u.Name)
                .MinimumLength(5).WithMessage("Name deve ter no mínimo 5 caracteres.")
                .Matches("^[a-z0-9]+$").WithMessage("Name deve estar em minúsculas e sem espaços.");
            });

            When(u => !string.IsNullOrEmpty(u.Password), () =>
            {
                RuleFor(u => u.Password)
                .MinimumLength(8).WithMessage("Senha deve ter no mínimo 8 caracteres.")
                .Matches("[0-9]").WithMessage("Senha deve ter pelo menos um número.")
                .Matches("[!@#$%^&*(),.?\":{}|<>]").WithMessage("Senha deve ter pelo menos um caracter especial.")
                .Matches("[A-Z]").WithMessage("Senha deve ter pelo menos uma letra maiúscula.");
            });
        }
    }
}
