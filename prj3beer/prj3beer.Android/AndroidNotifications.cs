using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using prj3beer.Services;

namespace prj3beer.Droid
{
    class AndroidNotifications : INotificationHandler
    {
        public NotificationType LastNotification { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool NotificationSent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void CompareTemp(double receivedTemp)
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public bool SendLocalNotification(string title, string body)
        {
            throw new NotImplementedException();
        }
    }
}