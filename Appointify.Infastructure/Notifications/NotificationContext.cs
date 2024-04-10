using Appointify.Domain.Notifications;

namespace Appointify.Infastructure.Notifications
{
    public class NotificationContext : INotificationContext
    {
        private readonly List<string> _notifications = new List<string>();

        public IReadOnlyCollection<string> Notifications => _notifications;

        public bool HasNotifications => _notifications.Any();

        public int ErrorCode { get; private set; }

        public void AddNotFound(string message)
        {
            _notifications.Add(message);
            ErrorCode = 404;
        }

        public void AddUnauthorized(string message)
        {
            _notifications.Add(message);
            ErrorCode = 401;
        }

        public void AddBadRequest(string message)
        {
            _notifications.Add(message);
            ErrorCode = 400;
        }
    }
}
