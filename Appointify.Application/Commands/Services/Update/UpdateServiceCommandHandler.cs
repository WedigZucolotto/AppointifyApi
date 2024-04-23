﻿using Appointify.Domain;
using Appointify.Domain.Authentication;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Commands.Services.Update
{
    public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, Nothing>
    {
        private readonly INotificationContext _notification;
        private readonly IServiceRepository _serviceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContext _httpContext;

        public UpdateServiceCommandHandler(
            INotificationContext notification,
            IServiceRepository serviceRepository,
            IUserRepository userRepository,
            IHttpContext httpContext) 
        {
            _notification = notification;
            _serviceRepository = serviceRepository;
            _userRepository = userRepository;
            _httpContext = httpContext;
        }

        public async Task<Nothing> Handle(UpdateServiceCommand command, CancellationToken cancellationToken) 
        {
            var service = await _serviceRepository.GetByIdAsync(command.Id);
            
            if (service == null)
            {
                _notification.AddNotFound("Serviço não encontrado.");
                return default;
            }

            var userClaims = _httpContext.GetUserClaims();

            var isUserCompany = await _userRepository
                .VerifyCompanyAsync(userClaims.Id ?? Guid.Empty, service.CompanyId);

            if (!isUserCompany)
            {
                _notification.AddBadRequest("Usuário não pertence à Empresa.");
                return default;
            }

            if (command.Interval != null)
            {
                var interval = TimeSpan.ParseExact(command.Interval, "hh\\:mm", null);
                service.Interval = interval;
            }

            service.Name = command.Name ?? service.Name;

            _serviceRepository.Update(service);
            await _serviceRepository.UnitOfWork.CommitAsync();

            return Nothing.Value;
        }
    }
}
