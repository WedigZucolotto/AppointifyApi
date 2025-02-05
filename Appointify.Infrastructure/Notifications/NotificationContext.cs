﻿using Appointify.Domain.Notifications;

namespace Appointify.Infrastructure.Notifications
{
    public class NotificationContext : INotificationContext
    {
        private readonly List<string> _notifications = new();

        public ICollection<string> Notifications => _notifications;

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
