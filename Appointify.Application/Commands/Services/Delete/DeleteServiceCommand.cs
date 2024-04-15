using Appointify.Domain;
using MediatR;

namespace Appointify.Application.Commands.Services.Delete
{
    public class DeleteServiceCommand : IRequest<Nothing>
    {
        public Guid Id { get; set; }

        public DeleteServiceCommand(Guid id)
        {
            Id = id;
        }
    }
}
