using FluentValidation;

namespace Appointify.Application.Commands.Events.Create
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidator() { }
    }
}
