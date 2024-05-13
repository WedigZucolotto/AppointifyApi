using FluentValidation;

namespace Appointify.Application.Commands.Events.Delete
{
    public class DeleteEventCommandValidator : AbstractValidator<DeleteEventCommand>
    {
        public DeleteEventCommandValidator() 
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Propriedade obrigatória: Id");
        }
    }
}
