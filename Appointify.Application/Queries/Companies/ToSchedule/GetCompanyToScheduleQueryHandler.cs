using Appointify.Application.Queries.Dtos;
using Appointify.Domain.Entities;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Companies.ToSchedule
{
    public class GetCompanyToScheduleQueryHandler : IRequestHandler<GetCompanyToScheduleQuery, GetCompanyToScheduleQueryResponse?>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly INotificationContext _notification;

        public GetCompanyToScheduleQueryHandler(ICompanyRepository companyRepository, INotificationContext notification)
        {
            _companyRepository = companyRepository;
            _notification = notification;
        }

        public async Task<GetCompanyToScheduleQueryResponse?> Handle(GetCompanyToScheduleQuery query, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(query.Id);

            if (company == null)
            {
                _notification.AddNotFound("Empresa não encontrada.");
                return default;
            }

            var minDate = DateTime.Today;
            var maxDate = minDate.AddDays(30);
            var unavaliableDates = GetUnavaliableDates(company);
            var showExtraFields = company.Plan.ShowExtraFields;
            var services = company.Services.Select(s => new OptionDto(s.Name, s.Id));

            return new GetCompanyToScheduleQueryResponse(
                minDate, 
                maxDate, 
                unavaliableDates, 
                showExtraFields, 
                services);
        }

        private List<DateTime> GetUnavaliableDates(Company company)
        {
            var today = DateTime.Today;
            var maxDate = today.AddDays(30);
            var unavaliableDates = new List<DateTime>();

            for (var day = today; day < maxDate; day = day.AddDays(1))
            {
                var date = new DateTime(day.Year, day.Month, day.Day);
                var hasAvailableTime = false;

                for (var time = company.Open; time < company.Close && !hasAvailableTime; time += TimeSpan.FromMinutes(1))
                {
                    date += time;

                    var _event = company.GetEvents().FirstOrDefault(e => e.Date == date);

                    if (_event != null)
                    {
                        time += _event.Service.Interval;
                        continue;
                    }
                    hasAvailableTime = true;
                }

                if (!hasAvailableTime)
                {
                    unavaliableDates.Add(date);
                }
            }
            return unavaliableDates;
        }
    }
}
