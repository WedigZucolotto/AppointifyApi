using FluentValidation;

namespace Appointify.Application.Queries.Events.ById
{
    public class GetEventByIdQueryValidator : AbstractValidator<GetEventByIdQuery>
    {
        public GetEventByIdQueryValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Propriedade obrigatória: Id");
        }
    }
}
