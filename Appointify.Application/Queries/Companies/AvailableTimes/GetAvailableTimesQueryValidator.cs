using FluentValidation;

namespace Appointify.Application.Queries.Companies.AvailableTimes
{
    public class GetAvailableTimesQueryValidator : AbstractValidator<GetAvailableTimesQuery>
    {
        public GetAvailableTimesQueryValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Propriedade obrigatória: Id");

            RuleFor(c => c.ServiceId)
                .NotEmpty().WithMessage("Propriedade obrigatória: ServiceId");

            RuleFor(c => c.Date)
                .NotEmpty().WithMessage("Propriedade obrigatória: Date")
                .Matches(@"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{2}$").WithMessage("Formato inválido.");
        }
    }
}
