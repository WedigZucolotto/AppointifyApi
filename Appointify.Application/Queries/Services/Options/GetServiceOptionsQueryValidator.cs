using FluentValidation;

namespace Appointify.Application.Queries.Services.Options
{
    public class GetServiceOptionsQueryValidator : AbstractValidator<GetServiceOptionsQuery>
    {
        public GetServiceOptionsQueryValidator() 
        {
            RuleFor(s => s.UserId)
                .NotEmpty().WithMessage("Propriedade obrigatória: UserId");
        }
    }
}
