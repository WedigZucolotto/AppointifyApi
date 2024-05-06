using Appointify.Application.Queries.Dtos;
using Appointify.Application.Queries.Users.Week;
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

            if (user == null)
            {
                _notification.AddNotFound("Usuário não encontrado.");
                return default;
            }

            var userClaims = _httpContext.GetUserClaims();

            if (user.Id != userClaims.Id)
            {
                _notification.AddUnauthorized("Você não tem permissão para realizar essa operação.");
                return default;
            }

            var culture = new CultureInfo("pt-BR");
            var queryDate = DateTime.Parse(query.Date, culture);

            var dayOfweek = (int)queryDate.DayOfWeek;
            var initialDate = queryDate.AddDays(-dayOfweek);
            var monthDays = DateTime.DaysInMonth(initialDate.Year, initialDate.Month);
            var dateWithMonth = initialDate.AddDays(monthDays);
            var finalDate = dateWithMonth.AddDays(7 - (int)dateWithMonth.DayOfWeek);

            var days = new List<GetUserMonthQueryResponse>();

            for (var date = initialDate; date <= finalDate; date = date.AddDays(1))
            {
                var dayEvents = new List<EventDto>();

                for (var hours = 0; hours < 24 || dayEvents.Count == 3; hours++)
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
                int? more = dayEvents.Count > 3 ? dayEvents.Count - 3 : null;

                days.Add(new GetUserMonthQueryResponse(day, weekNameFormated, dayEvents, more));
            }

            return days;
        }
    }
}
