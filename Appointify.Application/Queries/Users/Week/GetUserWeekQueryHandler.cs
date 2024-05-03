using Appointify.Application.Queries.Dtos;
using Appointify.Application.Queries.Users.Week;
using Appointify.Domain.Authentication;
using Appointify.Domain.Entities;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Appointify.Application.Queries.Users.ById
{
    public class GetUserWeekQueryHandler : IRequestHandler<GetUserWeekQuery, IEnumerable<GetUserWeekQueryResponse>?>
    {
        private readonly IUserRepository _userRepository;
        private readonly INotificationContext _notification;
        private readonly IHttpContext _httpContext;

        public GetUserWeekQueryHandler(
            IUserRepository userRepository, 
            INotificationContext notification,
            IHttpContext httpContext)
        {
            _userRepository = userRepository;
            _notification = notification;
            _httpContext = httpContext;
        }

        public async Task<IEnumerable<GetUserWeekQueryResponse>?> Handle(GetUserWeekQuery query, CancellationToken cancellationToken)
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
            var finalDate = initialDate.AddDays(6);

            var days = new List<GetUserWeekQueryResponse>();

            for (var date = initialDate; date <= finalDate; date = date.AddDays(1))
            {
                var hourEvents = new Dictionary<string, IEnumerable<EventDto>>();

                for (var hours = 1; hours < 24; hours++)
                {
                    var dateStart = date.AddHours(hours);
                    var dateEnd = dateStart.AddHours(1);

                    var events = user.Events
                        .Where(e => e.Date > dateStart && e.Date < dateEnd)
                        .Select(e => new EventDto(e.Id, e.Title, e.Date.ToString("HH:mm")));

                    hourEvents.Add(dateStart.ToString("HH:mm"), events);
                }

                var weekName = culture.DateTimeFormat.GetDayName(date.DayOfWeek);
                var weekNameFormated = weekName[..3].ToUpper();
                var day = date.Day.ToString();

                days.Add(new GetUserWeekQueryResponse(day, weekNameFormated, hourEvents));
            }

            return days;
        }
    }
}
