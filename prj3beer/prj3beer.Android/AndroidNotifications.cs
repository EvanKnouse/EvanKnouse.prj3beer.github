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

        private bool firstReading = true;

        public void CompareTemp(double receivedTemp, double idealTemp)
        {

            double dif = receivedTemp - idealTemp;

            NotificationType curType = default;
            if (firstReading)
            {
                if (dif > 0) curType = NotificationType.TOO_HOT;
                else curType = NotificationType.TOO_COLD;
                firstReading = false;
            }
            

            if (dif >= -1 || dif <= 1)
                curType = NotificationType.PERFECT;

            else if ((dif == 1 || dif == 2) && LastNotification != NotificationType.PERFECT)
                curType = NotificationType.IN_RANGE_HOT;

            else if ((dif == -1 || dif == -2) && LastNotification != NotificationType.PERFECT)
                curType = NotificationType.IN_RANGE_COLD;

            else if (dif >= 4)
                curType = NotificationType.TOO_HOT;

            else if (dif <= -4)
                curType = NotificationType.TOO_COLD;

            if(curType != LastNotification)
            {
                if (curType == NotificationType.TOO_COLD) SendLocalNotification("Cold Warning", "Your beverage is getting too cold");
                else if (curType == NotificationType.IN_RANGE_COLD) SendLocalNotification("Temperature Alert", "Your beverage is just below the desired temperature");
                else if (curType == NotificationType.PERFECT) SendLocalNotification("Drink Time!", "Your beverage has reached the perfect temperature");
                else if (curType == NotificationType.IN_RANGE_HOT) SendLocalNotification("Temperature Alert", "Your beverage is just above the desired temperature");
                else if (curType == NotificationType.TOO_HOT) SendLocalNotification("Heat Warning", "Your beverage is just below the desired temperature");
            }

            LastNotification = curType;
                
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