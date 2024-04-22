using FluentValidation;

namespace Appointify.Application.Commands.Users.Delete
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator() 
        {
            RuleFor(u => u.Id)
                .NotEmpty().WithMessage("Propriedade obrigatória: Id");
        }
    }
}
