using Appointify.Application.Queries.Companies.GetAll;
using FluentValidation;

namespace Appointify.Application.Queries.Companies.GetById
{
    public class GetAllCompaniesQueryValidator : AbstractValidator<GetAllCompaniesQuery>
    {
        public GetAllCompaniesQueryValidator() { }
    }
}
