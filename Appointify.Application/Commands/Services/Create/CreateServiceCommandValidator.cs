using FluentValidation;

namespace Appointify.Application.Commands.Services.Create
{
    public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
    {
        public CreateServiceCommandValidator() 
        { 
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("Propriedade obrigatória: Name");

            RuleFor(s => s.CompanyId)
                .NotEmpty().WithMessage("Propriedade obrigatória: CompanyId");

            RuleFor(s => s.Interval)
                .Matches(@"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$").WithMessage("Formato inválido: Interval")
                .NotEmpty().WithMessage("Propriedade obrigatória: Interval");
        }
    }
}
