using FluentValidation;

namespace Appointify.Application.Commands.Events.Create
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidator() 
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Propriedade obrigatória: Name");

            RuleFor(e => e.Contact)
                .NotEmpty().WithMessage("Propriedade obrigatória: Contact");

            RuleFor(c => c.Date)
                .NotEmpty().WithMessage("Propriedade obrigatória: Date")
                .Matches(@"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4} (?:[01]\d|2[0-3]):[0-5]\d$").WithMessage("Formato inválido.");

            RuleFor(e => e.ServiceId)
                .NotNull().WithMessage("Propriedade obrigatória: ServiceId");

            RuleFor(e => e.UserId)
                .NotNull().WithMessage("Propriedade obrigatória: UserId");
        }
    }
}
