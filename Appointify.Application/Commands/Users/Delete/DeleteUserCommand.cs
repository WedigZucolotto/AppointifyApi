using Appointify.Domain;
using MediatR;

namespace Appointify.Application.Commands.Users.Delete
{
    public class DeleteUserCommand : IRequest<Nothing>
    {
        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
