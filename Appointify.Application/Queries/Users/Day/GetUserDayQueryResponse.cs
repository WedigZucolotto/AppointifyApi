using Appointify.Application.Queries.Dtos;

namespace Appointify.Application.Queries.Users.Day
{
    public class GetUserDayQueryResponse
    {
        public GetUserDayQueryResponse(
            string day,
            string week,
            Dictionary<string, IEnumerable<EventDto>> events)
        {
            Day = day;
            Week = week;
            Events = events;
        }

        public string Day { get; set; } = string.Empty;

        public string Week { get; set; } = string.Empty;

        public Dictionary<string, IEnumerable<EventDto>> Events { get; set; }
    }
}
