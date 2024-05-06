using Appointify.Application.Queries.Dtos;

namespace Appointify.Application.Queries.Users.Month
{
    public class GetUserMonthQueryResponse
    {
        public GetUserMonthQueryResponse(
            string day,
            string week,
            IEnumerable<EventDto> events,
            int? more)
        {
            Day = day;
            Week = week;
            Events = events;
            More = more;
        }

        public string Day { get; set; } = string.Empty;

        public string Week { get; set; } = string.Empty;

        public IEnumerable<EventDto> Events { get; set; }

        public int? More { get; set; }
    }
}
