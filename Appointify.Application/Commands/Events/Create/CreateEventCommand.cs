using Appointify.Domain;
using MediatR;

namespace Appointify.Application.Commands.Events.Create
{
    public class CreateEventCommand : IRequest<Nothing>
    {
        public string Name { get; set; } = string.Empty;

        public string Contact { get; set; } = string.Empty;

        public string Date { get; set; } = string.Empty;

        public Guid? UserId { get; set; }
    }
}
