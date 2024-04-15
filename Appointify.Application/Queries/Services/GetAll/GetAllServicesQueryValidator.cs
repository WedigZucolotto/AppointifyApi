using FluentValidation;

namespace Appointify.Application.Queries.Services.GetAll
{
    public class GetAllServicesQueryValidator : AbstractValidator<GetAllServicesQuery>
    {
        public GetAllServicesQueryValidator() 
        {
            RuleFor(s => s.CompanyId)
                .NotEmpty();
        }
    }
}
