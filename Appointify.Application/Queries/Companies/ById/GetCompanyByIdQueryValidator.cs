using FluentValidation;

namespace Appointify.Application.Queries.Companies.ById
{
    public class GetCompanyByIdQueryValidator : AbstractValidator<GetCompanyByIdQuery>
    {
        public GetCompanyByIdQueryValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Propriedade obrigatória: Id");
        }
    }
}
