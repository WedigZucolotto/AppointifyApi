using Appointify.Application.Queries.Dtos;
using Appointify.Domain.Entities;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;
using System.Numerics;

namespace Appointify.Application.Queries.Companies
{
    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, GetCompanyByIdQueryResponse?>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly INotificationContext _notification;

        public GetCompanyByIdQueryHandler(ICompanyRepository companyRepository, INotificationContext notification)
        {
            _companyRepository = companyRepository;
            _notification = notification;
        }

        public async Task<GetCompanyByIdQueryResponse?> Handle(GetCompanyByIdQuery query, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(query.Id);

            if (company == null)
            {
                _notification.AddNotFound("Company does not exists");
                return default;
            }

            var minDate = DateTime.Today;
            var maxDate = minDate.AddDays(company.DayLimit);
            var unavaliableDates = GetUnavaliableDates(company);
            var showExtraFields = company.Plan.ShowExtraFields;
            var services = company.Services.Select(s => new ServiceDto(s.Id, s.Name));

            return new GetCompanyByIdQueryResponse(minDate, maxDate, unavaliableDates, showExtraFields, services);
        }

        private List<DateTime> GetUnavaliableDates(Company company)
        {
            var today = DateTime.Today;
            var maxDate = today.AddDays(company.DayLimit);
            var unavaliableDates = new List<DateTime>();

            for (var day = today; day < maxDate; day = day.AddDays(1))
            {
                var date = new DateTime(day.Year, day.Month, day.Day);
                var hasAvailableTime = false;

                for (var time = company.Open; time < company.Close && !hasAvailableTime; time += TimeSpan.FromMinutes(10))
                {
                    date += time;

                    var _event = company.Events.FirstOrDefault(e => e.Date == date);

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
