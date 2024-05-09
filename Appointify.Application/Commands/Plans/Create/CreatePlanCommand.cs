using Appointify.Domain;
using MediatR;

namespace Appointify.Application.Commands.Plans.Create
{
    public class CreatePlanCommand : IRequest<Nothing>
    {
        public string Name { get; set; } = string.Empty;

        public bool ShowExtraFields { get; set; }
    }
}
