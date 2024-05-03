using FluentValidation;

namespace Appointify.Application.Queries.Users.Week
{
    public class GetUserWeekQueryValidator : AbstractValidator<GetUserWeekQuery>
    {
        public GetUserWeekQueryValidator() 
        {
            RuleFor(u => u.Id)
                .NotEmpty().WithMessage("Propriedade obrigatória: Id");

            RuleFor(u => u.Date)
                .NotNull().WithMessage("Propriedade obrigatória: Date")
                .Matches(@"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$").WithMessage("Formato inválido."); ;
        }
    }
}
