using Appointify.Domain;
using MediatR;

namespace Appointify.Application.Commands.Services.Create
{
    public class CreateServiceCommand : IRequest<Nothing>
    {
        public string Name { get; set; } = string.Empty;

        public string Interval { get; set; } = string.Empty;

        public Guid CompanyId { get; set; }
    }
}
