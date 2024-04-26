using Appointify.Domain.Entities.Enums;
using MediatR;

namespace Appointify.Application.Queries.Users.All
{
    public class GetAllUsersQuery : IRequest<IEnumerable<GetAllUsersQueryResponse>>
    {
        public string? Name { get; set; }

        public string? CompleteName { get; set; }

        public UserType? Type { get; set; } 

        public Guid? CompanyId { get; set; }
    }
}
