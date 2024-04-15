using Appointify.Domain;
using MediatR;

namespace Appointify.Application.Commands.Companies.Create
{
    public class CreateCompanyCommand : IRequest<Nothing>
    {
        public string Name { get; set; } = string.Empty;

        public string Open { get; set; } = string.Empty;

        public string Close { get; set; } = string.Empty;

        public Guid PlanId { get; set; }
    }
}
