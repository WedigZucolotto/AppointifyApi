using FluentValidation;

namespace Appointify.Application.Queries.Companies.All
{
    public class GetAllCompaniesQueryValidator : AbstractValidator<GetAllCompaniesQuery>
    {
        public GetAllCompaniesQueryValidator() { }
    }
}
