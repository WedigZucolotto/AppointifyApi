using Appointify.Domain;
using MediatR;

namespace Appointify.Application.Commands.Companies.Delete
{
    public class DeleteCompanyCommand : IRequest<Nothing>
    {
        public DeleteCompanyCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
