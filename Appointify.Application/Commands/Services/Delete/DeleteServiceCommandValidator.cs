using FluentValidation;

namespace Appointify.Application.Commands.Services.Delete
{
    public class DeleteServiceCommandValidator : AbstractValidator<DeleteServiceCommand>
    {
        public DeleteServiceCommandValidator() 
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Propriedade obrigatória: Id");
        }
    }
}
