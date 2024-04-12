using FluentValidation;

namespace Appointify.Application.Queries.Companies
{
    public class GetCompanyByIdQueryValidator : AbstractValidator<GetCompanyByIdQuery>
    {
        public GetCompanyByIdQueryValidator() 
        {
            RuleFor(c => c.Id)
                .NotEmpty();
        }
    }
}
