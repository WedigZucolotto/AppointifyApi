using FluentValidation;

namespace Appointify.Application.Queries.Companies.Services
{
    public class GetCompanyServicesQueryValidator : AbstractValidator<GetCompanyServicesQuery>
    {
        public GetCompanyServicesQueryValidator()
        {
            RuleFor(u => u.Id)
                .NotEmpty().WithMessage("Propriedade obrigatória: Id");
        }
    }
}
