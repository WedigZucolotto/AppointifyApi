using FluentValidation;

namespace Appointify.Application.Commands.Services.Create
{
    public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
    {
        public CreateServiceCommandValidator() 
        { 
            RuleFor(s => s.Name)
                .NotEmpty();

            RuleFor(s => s.CompanyId)
                .NotEmpty();

            RuleFor(s => s.Interval)
                .Matches(@"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$")
                .NotEmpty();
        }
    }
}
