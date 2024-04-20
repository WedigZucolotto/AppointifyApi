using FluentValidation;

namespace Appointify.Application.Commands.Companies.Delete
{
    public class DeleteCompanyCommandValidator : AbstractValidator<DeleteCompanyCommand>
    {
        public DeleteCompanyCommandValidator() 
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Propriedade obrigatória: Id");
        }
    }
}
