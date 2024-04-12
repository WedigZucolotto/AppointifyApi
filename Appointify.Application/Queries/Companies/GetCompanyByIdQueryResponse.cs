using Appointify.Application.Queries.Dtos;

namespace Appointify.Application.Queries.Companies
{
    public class GetCompanyByIdQueryResponse
    {
        public GetCompanyByIdQueryResponse(
            DateTime minDate, 
            DateTime maxDate,
            List<DateTime> unavailableDates,
            bool showExtraFields,
            IEnumerable<ServiceDto> services)
        {
            MinDate = minDate;
            MaxDate = maxDate;
            UnavailableDates = unavailableDates;
            ShowExtraFields = showExtraFields;
            Services = services;
        }

        public DateTime MinDate { get; set; }

        public DateTime MaxDate { get; set; }

        public List<DateTime> UnavailableDates { get; set; }

        public bool ShowExtraFields { get; set; }

        public IEnumerable<ServiceDto> Services { get; set; }
    }
}
