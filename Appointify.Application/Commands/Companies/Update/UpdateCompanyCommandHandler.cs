﻿using Appointify.Domain;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Commands.Companies.Update
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Nothing>
    {
        private readonly INotificationContext _notification;
        private readonly IPlanRepository _planRepository;
        private readonly ICompanyRepository _companyRepository;

        public UpdateCompanyCommandHandler(
            INotificationContext notification, 
            IPlanRepository planRepository,
            ICompanyRepository companyRepository) 
        {
            _notification = notification;
            _planRepository = planRepository;
            _companyRepository = companyRepository;
        }

        public async Task<Nothing> Handle(UpdateCompanyCommand command, CancellationToken cancellationToken) 
        {
            var company = await _companyRepository.GetByIdAsync(command.Id);
            
            if (company == null)
            {
                _notification.AddNotFound("Company does not exists");
                return default;
            }

            if (command.PlanId != null)
            {
                var plan = await _planRepository.GetByIdAsync(command.PlanId.Value);

                if (plan == null)
                {
                    _notification.AddNotFound("Plan does not exists");
                    return default;
                }

                company.Plan = plan;
                company.PlanId = plan.Id;
            }

            if (command.Open != null)
            {
                var open = TimeSpan.ParseExact(command.Open, "hh\\:mm", null);
                company.Open = open;
            }

            if (command.Close != null)
            {
                var close = TimeSpan.ParseExact(command.Close, "hh\\:mm", null);
                company.Close = close;
            }

            company.Name = command.Name ?? company.Name;

            _companyRepository.Update(company);
            await _companyRepository.UnitOfWork.CommitAsync();

            return Nothing.Value;
        }
    }
}