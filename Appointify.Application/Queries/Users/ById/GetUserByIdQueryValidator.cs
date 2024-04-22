using Appointify.Application.Queries.Users.ById;
using FluentValidation;

namespace Appointify.Application.Queries.Users.ById
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator() 
        {
            RuleFor(u => u.Id)
                .NotEmpty().WithMessage("Propriedade obrigatória: Id");
        }
    }
}
