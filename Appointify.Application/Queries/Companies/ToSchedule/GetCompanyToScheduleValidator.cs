using FluentValidation;

namespace Appointify.Application.Queries.Companies.ToSchedule
{
    public class GetCompanyToScheduleValidator : AbstractValidator<GetCompanyToScheduleQuery>
    {
        public GetCompanyToScheduleValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Propriedade obrigatória: Id");
        }
    }
}
