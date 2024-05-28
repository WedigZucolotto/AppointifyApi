using Appointify.Application.Queries.Dtos;
using Appointify.Domain.Authentication;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;
using System.Globalization;

namespace Appointify.Application.Queries.Users.Month
{
    public class GetUserMonthQueryHandler : IRequestHandler<GetUserMonthQuery, IEnumerable<GetUserMonthQueryResponse>?>
    {
        private readonly IUserRepository _userRepository;
        private readonly INotificationContext _notification;
        private readonly IHttpContext _httpContext;

        public GetUserMonthQueryHandler(
            IUserRepository userRepository,
            INotificationContext notification,
            IHttpContext httpContext)
        {
            _userRepository = userRepository;
            _notification = notification;
            _httpContext = httpContext;
        }

        public async Task<IEnumerable<GetUserMonthQueryResponse>?> Handle(GetUserMonthQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(query.Id);
            var userId = _httpContext.GetUserId();

            if (user == null || userId != query.Id)
            {
                _notification.AddNotFound("Usuário não encontrado.");
                return default;
            }

            var culture = new CultureInfo("pt-BR");
            var queryDate = DateTime.Parse(query.Date, culture);

            var monthDays = DateTime.DaysInMonth(queryDate.Year, queryDate.Month);
            var firstDayMonth = new DateTime(queryDate.Year, queryDate.Month, 1);
            var lastDayMonth = new DateTime(queryDate.Year, queryDate.Month, monthDays);

            var firstDayWeek = (int)firstDayMonth.DayOfWeek;
            var lastDayWeek = (int)lastDayMonth.DayOfWeek;

            var initialDate = firstDayMonth.AddDays(-firstDayWeek);
            var finalDate = lastDayMonth.AddDays(6 - lastDayWeek);

            var days = new List<GetUserMonthQueryResponse>();

            for (var date = initialDate; date <= finalDate; date = date.AddDays(1))
            {
                var dayEvents = new List<EventDto>();

                for (var hours = 0; hours < 24; hours++)
                {
                    var dateStart = date.AddHours(hours);
                    var dateEnd = dateStart.AddHours(1);

                    foreach (var _event in user.Events)
                    {
                        if (_event.Date >= dateStart && _event.Date < dateEnd)
                        {
                            var newEvent = new EventDto(_event.Id, _event.Title, _event.Date.ToString("HH:mm"));
                            dayEvents.Add(newEvent);
                        }
                    }
                }

                var weekName = culture.DateTimeFormat.GetDayName(date.DayOfWeek);
                var weekNameFormated = weekName[..3].ToUpper();
                var day = date.Day.ToString();
                var isPastDate = date < DateTime.Today;
                int? more = dayEvents.Count > 3 ? dayEvents.Count - 3 : null;

                days.Add(new GetUserMonthQueryResponse(day, weekNameFormated, dayEvents.Take(3), isPastDate, more));
            }

            return days;
        }
    }
}
