using Appointify.Domain;
using MediatR;

namespace Appointify.Application.Commands.Companies.Delete
{
    public class DeleteCompanyCommand : IRequest<Nothing>
    {
        public Guid Id { get; set; }

        public DeleteCompanyCommand(Guid id)
        {
            Id = id;
        }
    }
}
