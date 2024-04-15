using FluentValidation;

namespace Appointify.Application.Queries.Companies.GetAll
{
    public class GetAllCompaniesQueryValidator : AbstractValidator<GetAllCompaniesQuery>
    {
        public GetAllCompaniesQueryValidator() { }
    }
}
