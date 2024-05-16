using Appointify.Application.Queries.Dtos;
using Appointify.Domain.Authentication;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;
using System.Globalization;

namespace Appointify.Application.Queries.Users.Day
{
    public class GetUserDayQueryHandler : IRequestHandler<GetUserDayQuery, GetUserDayQueryResponse?>
    {
        private readonly IUserRepository _userRepository;
        private readonly INotificationContext _notification;
        private readonly IHttpContext _httpContext;

        public GetUserDayQueryHandler(
            IUserRepository userRepository,
            INotificationContext notification,
            IHttpContext httpContext)
        {
            _userRepository = userRepository;
            _notification = notification;
            _httpContext = httpContext;
        }

        public async Task<GetUserDayQueryResponse?> Handle(GetUserDayQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(query.Id);
            var userId = _httpContext.GetUserId();

            if (user == null || userId != query.Id)
            {
                _notification.AddNotFound("Usuário não encontrado.");
                return default;
            }

            var culture = new CultureInfo("pt-BR");
            var date = DateTime.Parse(query.Date, culture);

            var hourEvents = new Dictionary<string, IEnumerable<EventDto>>();

            for (var hours = 0; hours < 24; hours++)
            {
                var dateStart = date.AddHours(hours);
                var dateEnd = dateStart.AddHours(1);

                var events = user.Events
                    .Where(e => e.Date >= dateStart && e.Date < dateEnd)
                    .Select(e => new EventDto(e.Id, e.Title, e.Date.ToString("HH:mm")));

                hourEvents.Add(dateStart.ToString("HH:mm"), events);
            }

            var weekName = culture.DateTimeFormat.GetDayName(date.DayOfWeek);
            var weekNameFormated = weekName[..3].ToUpper();
            var day = date.Day.ToString();

            return new GetUserDayQueryResponse(day, weekNameFormated, hourEvents);
        }
    }
}
