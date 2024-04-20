using Appointify.Domain;
using MediatR;

namespace Appointify.Application.Commands.Services.Delete
{
    public class DeleteServiceCommand : IRequest<Nothing>
    {
        public DeleteServiceCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
