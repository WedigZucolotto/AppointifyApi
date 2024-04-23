using FluentValidation;

namespace Appointify.Application.Commands.Companies.Create
{
    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyCommandValidator() 
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Propriedade obrigatória: Name");

            RuleFor(c => c.Open)
               .Matches(@"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$").WithMessage("Formato inválido: Open")
               .NotEmpty().WithMessage("Propriedade obrigatória: Open");

            RuleFor(c => c.Close)
               .Matches(@"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$").WithMessage("Formato inválido: Close")
               .NotEmpty().WithMessage("Propriedade obrigatória: Close")
               .GreaterThan(c => c.Open).WithMessage("Close deve ser maior que o Open.");

            RuleFor(c => c.PlanId)
               .NotEmpty().WithMessage("Propriedade obrigatória: PlanId");
        }
    }
}
