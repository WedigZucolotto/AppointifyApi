using Appointify.Domain.Notifications;
using FluentValidation;
using MediatR;

namespace Appointify.Infastructure.Notifications
{
    public class PipelineValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IValidator<TRequest> _validator;
        private readonly INotificationContext _notificationContext;

        public PipelineValidationBehavior(
            INotificationContext notificationContext,
            IValidator<TRequest> validator)
        {
            _notificationContext = notificationContext;
            _validator = validator;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);

            if (result.IsValid)
            {
                return next();
            }

            foreach (var error in result.Errors)
            {
                _notificationContext.AddBadRequest(error.ErrorMessage);
            }

            return Task.FromResult(default(TResponse));
        }
    }
}
