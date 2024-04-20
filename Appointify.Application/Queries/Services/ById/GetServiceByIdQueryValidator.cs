using FluentValidation;

namespace Appointify.Application.Queries.Services.ById
{
    public class GetServiceByIdQueryValidator : AbstractValidator<GetServiceByIdQuery>
    {
        public GetServiceByIdQueryValidator()
        {
            RuleFor(s => s.Id)
                .NotEmpty().WithMessage("Propriedade obrigatória: Id");
        }
    }
}
