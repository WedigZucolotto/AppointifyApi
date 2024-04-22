using FluentValidation;

namespace Appointify.Application.Queries.Users.All
{
    public class GetAllUsersQueryValidator : AbstractValidator<GetAllUsersQuery>
    {
        public GetAllUsersQueryValidator() { }
    }
}
