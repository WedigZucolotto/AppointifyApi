using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Companies.AvailableTimes
{
    public class GetCompanyByIdQueryHandler : IRequestHandler<GetAvailableTimesQuery, IEnumerable<string>>
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

        public async Task<IEnumerable<string>> Handle(GetAvailableTimesQuery query, CancellationToken cancellationToken)
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

            var isCompanyService = company.Services.Any(s => s.Id == query.ServiceId);

            if (!isCompanyService)
            {
                _notification.AddBadRequest("Serviço não viculado a empresa.");
                return default;
            }

            var events = company.Events();

            if (query.UserId != null)
            {
                var user = company.Users.FirstOrDefault(u => u.Id == query.Id);

                if (user == null)
                {
                    _notification.AddNotFound("Usuário não encontrado.");
                    return default;
                }

                events = user.Events;
            }

            var intialDate = DateTime.Parse(query.Date);
            var accumulatedTime = new TimeSpan();
            var availableTimes = new List<TimeSpan>();

            // quando for da company => fazer for com funcionarios
            for (var time = company.Open; time < company.Close; time += TimeSpan.FromMinutes(1))
            {
                var date = intialDate.Add(time);
                var _event = events.FirstOrDefault(e => e.Date == date);

                if (_event != null)
                {
                    time += _event.Service.Interval;
                    accumulatedTime = new TimeSpan();
                    continue;
                }

                if (service.Interval == accumulatedTime)
                {
                    availableTimes.Add(time);
                    accumulatedTime = new TimeSpan();
                }

                accumulatedTime += TimeSpan.FromMinutes(1);
            }

            return availableTimes.Select(time => time.ToString(@"hh\:mm"));
        }
    }
}
