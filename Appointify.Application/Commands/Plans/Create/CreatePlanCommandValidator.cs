using FluentValidation;

namespace Appointify.Application.Commands.Plans.Create
{
    public class CreatePlanCommandValidator : AbstractValidator<CreatePlanCommand>
    {
        public CreatePlanCommandValidator() 
        { 
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Propriedade obrigatória: Name");

            RuleFor(p => p.ShowExtraFields)
                .NotNull().WithMessage("Propriedade obrigatória: ShowExtraFields");
        }
    }
}
