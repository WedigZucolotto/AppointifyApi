using Appointify.Domain;
using Appointify.Domain.Entities;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Commands.Services.Create
{
    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, Nothing>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly INotificationContext _notification;

        public CreateServiceCommandHandler(
            IServiceRepository serviceRepository, 
            ICompanyRepository companyRepository,
            INotificationContext notification) 
        {
            _serviceRepository = serviceRepository;
            _companyRepository = companyRepository;
            _notification = notification;
        }

        public async Task<Nothing> Handle(CreateServiceCommand command, CancellationToken cancellationToken) 
        {
            var company = await _companyRepository.GetByIdAsync(command.CompanyId);

            if (company == null)
            {
                _notification.AddNotFound("Company does not exists");
                return default;
            }

            var interval = TimeSpan.ParseExact(command.Interval, "hh\\:mm", null);

            var service = new Service(command.Name, interval, company);

            _serviceRepository.Add(service);
            await _serviceRepository.UnitOfWork.CommitAsync();

            return Nothing.Value;
        }
    }
}
