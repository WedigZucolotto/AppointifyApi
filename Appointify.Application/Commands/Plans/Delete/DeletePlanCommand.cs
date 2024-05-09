using Appointify.Domain;
using MediatR;

namespace Appointify.Application.Commands.Plans.Delete
{
    public class DeletePlanCommand : IRequest<Nothing>
    {
        public DeletePlanCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
