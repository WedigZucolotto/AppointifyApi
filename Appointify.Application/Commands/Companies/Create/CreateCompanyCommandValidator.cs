using FluentValidation;

namespace Appointify.Application.Commands.Companies.Create
{
    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyCommandValidator() 
        {
            RuleFor(c => c.Name)
                .NotEmpty();

            RuleFor(c => c.Open)
               .Matches(@"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$")
               .NotEmpty();

            RuleFor(c => c.Close)
               .Matches(@"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$")
               .NotEmpty();

            RuleFor(c => c.PlanId)
               .NotEmpty();
        }
    }
}
