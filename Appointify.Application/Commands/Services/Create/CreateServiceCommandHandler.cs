using Appointify.Domain;
using Appointify.Domain.Authentication;
using Appointify.Domain.Entities;
using Appointify.Domain.Entities.Enums;
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
        private readonly IHttpContext _httpContext;

        public CreateServiceCommandHandler(
            IServiceRepository serviceRepository,
            ICompanyRepository companyRepository,
            INotificationContext notification,
            IHttpContext httpContext) 
        {
            _serviceRepository = serviceRepository;
            _companyRepository = companyRepository;
            _notification = notification;
            _httpContext = httpContext;
        }

        public async Task<Nothing> Handle(CreateServiceCommand command, CancellationToken cancellationToken) 
        {
            var company = await _companyRepository.GetByIdAsync(command.CompanyId);

            if (company == null)
            {
                _notification.AddNotFound("Empresa não encontrada.");
                return default;
            }

            var userClaims = _httpContext.GetUserClaims();

            var canEditCompany = company.CanEdit(userClaims);

            if (!canEditCompany)
            {
                _notification.AddUnauthorized("Você não tem permissão para realizar essa operação.");
                return default;
            }

            var isStandard = company.IsStandard();

            if (isStandard)
            {
                _notification.AddBadRequest("Plano insuficiente.");
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
