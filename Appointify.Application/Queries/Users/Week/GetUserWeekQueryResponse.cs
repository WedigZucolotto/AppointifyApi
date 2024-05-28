using Appointify.Application.Queries.Dtos;

namespace Appointify.Application.Queries.Users.Week
{
    public class GetUserWeekQueryResponse
    {
        public GetUserWeekQueryResponse(
            string day,
            string week,
            Dictionary<string, IEnumerable<EventDto>> events,
            bool isPastDate)
        {
            Day = day;
            Week = week;
            Events = events;
            IsPastDate = isPastDate;
        }

        public string Day { get; set; } = string.Empty;

        public string Week { get; set; } = string.Empty;

        public Dictionary<string, IEnumerable<EventDto>> Events { get; set; }

        public bool IsPastDate { get; set; }
    }
}
