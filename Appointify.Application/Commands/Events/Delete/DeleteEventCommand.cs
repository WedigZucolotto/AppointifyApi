using Appointify.Domain;
using MediatR;

namespace Appointify.Application.Commands.Events.Delete
{
    public class DeleteEventCommand : IRequest<Nothing>
    {
        public DeleteEventCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
