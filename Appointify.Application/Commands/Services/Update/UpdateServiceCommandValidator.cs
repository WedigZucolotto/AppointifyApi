using FluentValidation;

namespace Appointify.Application.Commands.Services.Update
{
    public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
    {
        public UpdateServiceCommandValidator() 
        {
            RuleFor(s => s.Id)
                .NotEmpty().WithMessage("Propriedade obrigatória: Id");

            RuleFor(s => s.Interval)
                .Matches(@"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$")
                .WithMessage("Formato inválido: Interval");
        }
    }
}
