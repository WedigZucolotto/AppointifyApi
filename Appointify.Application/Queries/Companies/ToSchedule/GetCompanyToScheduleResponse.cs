using Appointify.Application.Queries.Dtos;

namespace Appointify.Application.Queries.Companies.ToSchedule
{
    public class GetCompanyToScheduleQueryResponse
    {
        public GetCompanyToScheduleQueryResponse(
            DateTime maxDate,
            List<DateTime> unavailableDates,
            bool showExtraFields,
            IEnumerable<OptionDto> services)
        {
            MaxDate = maxDate;
            UnavailableDates = unavailableDates;
            ShowExtraFields = showExtraFields;
            Services = services;
        }

        public DateTime MaxDate { get; set; }

        public List<DateTime> UnavailableDates { get; set; }

        public bool ShowExtraFields { get; set; }

        public IEnumerable<OptionDto> Services { get; set; }
    }
}
