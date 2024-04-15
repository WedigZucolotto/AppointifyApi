using Appointify.Domain;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Commands.Companies.Delete
{
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, Nothing>
    {
        private readonly INotificationContext _notification;
        private readonly ICompanyRepository _companyRepository;

        public DeleteCompanyCommandHandler(
            INotificationContext notification,
            ICompanyRepository companyRepository) 
        {
            _notification = notification;
            _companyRepository = companyRepository;
        }

        public async Task<Nothing> Handle(DeleteCompanyCommand command, CancellationToken cancellationToken) 
        {
            var company = await _companyRepository.GetByIdAsync(command.Id);
            
            if (company == null)
            {
                _notification.AddNotFound("Company does not exists");
                return default;
            }

            _companyRepository.Remove(company);
            await _companyRepository.UnitOfWork.CommitAsync();

            return Nothing.Value;
        }
    }
}
