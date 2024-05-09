using FluentValidation;

namespace Appointify.Application.Commands.Plans.Update
{
    public class UpdatePlanCommandValidator : AbstractValidator<UpdatePlanCommand>
    {
        public UpdatePlanCommandValidator() 
        {
            RuleFor(s => s.Id)
                .NotEmpty().WithMessage("Propriedade obrigatória: Id");
        }
    }
}
