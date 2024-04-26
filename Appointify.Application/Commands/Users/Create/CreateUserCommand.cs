using Appointify.Domain;
using MediatR;

namespace Appointify.Application.Commands.Users.Create
{
    public class CreateUserCommand : IRequest<Nothing>
    {
        public string Name { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string CompleteName { get; set; } = string.Empty;

        public Guid CompanyId { get; set; }

        public bool IsOwner { get; set; }
    }
}
