using FluentValidation;

namespace Appointify.Application.Queries.Plans.ById
{
    public class GetPlanByIdQueryValidator : AbstractValidator<GetPlanByIdQuery>
    {
        public GetPlanByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Propriedade obrigatória: Id");
        }
    }
}
