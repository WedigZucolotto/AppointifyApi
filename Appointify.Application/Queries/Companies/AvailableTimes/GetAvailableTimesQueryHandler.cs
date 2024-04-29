using Appointify.Application.Queries.Dtos;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Companies.AvailableTimes
{
    public class GetCompanyByIdQueryHandler : IRequestHandler<GetAvailableTimesQuery, IEnumerable<AvailableTimeDto>?>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly INotificationContext _notification;

        public GetCompanyByIdQueryHandler(
            ICompanyRepository companyRepository, 
            IServiceRepository serviceRepository,
            INotificationContext notification)
        {
            _companyRepository = companyRepository;
            _serviceRepository = serviceRepository;
            _notification = notification;
        }

        public async Task<IEnumerable<AvailableTimeDto>?> Handle(GetAvailableTimesQuery query, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(query.Id);

            if (company == null)
            {
                _notification.AddNotFound("Empresa não encontrada.");
                return default;
            }

            var service = await _serviceRepository.GetByIdAsync(query.ServiceId);

            if (service == null)
            {
                _notification.AddNotFound("Serviço não encontrado.");
                return default;
            }

            if (service.CompanyId != company.Id)
            {
                _notification.AddBadRequest("Serviço não viculado a empresa.");
                return default;
            }

            var userSelected = query.UserId != null;
            var date = DateTime.Parse(query.Date);

            if (userSelected)
            {
                var user = company.Users.FirstOrDefault(u => u.Id == query.UserId);

                if (user == null)
                {
                    _notification.AddNotFound("Usuário não encontrado.");
                    return default;
                };

                var userTimes = user.GetAvailableTimes(date, service.Interval);
                return userTimes.Select(time => new AvailableTimeDto(time.ToString(@"hh\:mm"), user.Id));
            }

            var companyTimes = company.GetAvailableTimes(date, service.Interval);
            return companyTimes.Select(at => new AvailableTimeDto(at.Time.ToString(@"hh\:mm"), at.UserId));
        }
    }
}
