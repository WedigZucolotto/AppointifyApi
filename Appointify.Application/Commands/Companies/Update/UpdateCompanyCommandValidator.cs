using FluentValidation;

namespace Appointify.Application.Commands.Companies.Update
{
    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
    {
        public UpdateCompanyCommandValidator() 
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Propriedade obrigatória: Id");

            RuleFor(c => c.Open)
               .Matches(@"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$")
               .WithMessage("Formato inválido: Open");

            RuleFor(c => c.Close)
               .Matches(@"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$")
               .WithMessage("Formato inválido: Close");
        }
    }
}
