using FluentValidation;

namespace Appointify.Application.Queries.Events.Day
{
    public class GetDayEventsQueryValidator : AbstractValidator<GetDayEventsQuery>
    {
        public GetDayEventsQueryValidator() { }
    }
}
