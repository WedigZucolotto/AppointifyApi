using FluentValidation;

namespace Appointify.Application.Commands.Plans.Delete
{
    public class DeletePlanCommandValidator : AbstractValidator<DeletePlanCommand>
    {
        public DeletePlanCommandValidator() 
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Propriedade obrigatória: Id");
        }
    }
}
