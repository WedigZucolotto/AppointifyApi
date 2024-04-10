using Appointify.Domain;
using Appointify.Domain.Notifications;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Appointify.Api.Filters
{
    public class NotificationFilter : IAsyncResultFilter
    {
        private readonly INotificationContext _notificationContext;

        public NotificationFilter(INotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (_notificationContext.HasNotifications)
            {
                context.HttpContext.Response.StatusCode = _notificationContext.ErrorCode;
                context.HttpContext.Response.ContentType = "application/json";

                var error = new ErrorResponse("An error has occurred", _notificationContext.ErrorCode, _notificationContext.Notifications);
                var serializedError = JsonConvert.SerializeObject(error);

                await context.HttpContext.Response.WriteAsync(serializedError);
                return;
            }

            await next();
        }
    }
}
