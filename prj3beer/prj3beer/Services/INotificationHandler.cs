using System;
using System.Collections.Generic;
using System.Text;

namespace prj3beer.Services
{
    public interface INotificationHandler
    {
        NotificationType LastNotification { get; set; }
        bool NotificationSent { get; set; }

        void Initialize();

        bool SendLocalNotification(string title, string body);

        void CompareTemp(double receivedTemp);
    }
}