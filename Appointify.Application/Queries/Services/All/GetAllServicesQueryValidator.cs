using FluentValidation;

namespace Appointify.Application.Queries.Services.All
{
    public class GetAllServicesQueryValidator : AbstractValidator<GetAllServicesQuery>
    {
        public GetAllServicesQueryValidator()
        {
            RuleFor(s => s.CompanyId)
                .NotEmpty().WithMessage("Propriedade obrigatória: CompanyId");
        }
    }
}
