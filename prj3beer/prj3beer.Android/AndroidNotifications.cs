using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

using prj3beer.Services;
using Xamarin.Forms;
using AndroidApp = Android.App.Application;

[assembly: Dependency(typeof(prj3beer.Droid.AndroidNotifications))]
namespace prj3beer.Droid
{
    class AndroidNotifications : INotificationHandler
    {
        const string channelId = "default";
        const string channelName = "Default";
        const string channelDescription = "The default channel for notifications.";

        public const string TitleKey = "title";
        public const string MessageKey = "body";

        const int pendingIntentId = 0;
        int messageId = -1;
        NotificationManager manager;

        bool channelInitialized = false;

        public NotificationType LastNotification { get; set; }
        public bool NotificationSent { get; set; }

        private bool firstReading = true;

        public bool CompareTemp(double receivedTemp, double idealTemp)
        {

            NotificationSent = false;

            double dif = receivedTemp - idealTemp;

            NotificationType curType = default;
            if (firstReading)
            {
                if (dif > 0) curType = NotificationType.TOO_HOT;
                else curType = NotificationType.TOO_COLD;
                LastNotification = curType;
                firstReading = false;
            }
            

            if (dif == 0)
                curType = NotificationType.PERFECT;

            else if ((dif == 1 || dif == 2) && !(LastNotification > NotificationType.TOO_HOT && LastNotification < NotificationType.TOO_COLD))
                curType = NotificationType.IN_RANGE_HOT;

            else if ((dif == -1 || dif == -2) && !(LastNotification > NotificationType.TOO_HOT && LastNotification < NotificationType.TOO_COLD))
                curType = NotificationType.IN_RANGE_COLD;

            else if (dif >= 4)
                curType = NotificationType.TOO_HOT;

            else if (dif <= -4)
                curType = NotificationType.TOO_COLD;

            if(curType != NotificationType.NO_MESSAGE && curType != LastNotification)
            {
                if (curType == NotificationType.TOO_COLD) SendLocalNotification("Cold Warning", "Your beverage is getting too cold.");
                else if (curType == NotificationType.IN_RANGE_COLD) SendLocalNotification("Temperature Alert", "Your beverage is just below the desired temperature.");
                else if (curType == NotificationType.PERFECT) SendLocalNotification("Drink Time!", "Your beverage has reached the perfect temperature.");
                else if (curType == NotificationType.IN_RANGE_HOT) SendLocalNotification("Temperature Alert", "Your beverage is just above the desired temperature.");
                else if (curType == NotificationType.TOO_HOT) SendLocalNotification("Heat Warning", "Your beverage is getting too hot.");

                LastNotification = curType;
                NotificationSent = true;
            }

            return NotificationSent;    

        }

        public void Initialize()
        {
            CreateNotificationChannel();
        }

        void CreateNotificationChannel()
        {
            manager = (NotificationManager)AndroidApp.Context.GetSystemService(AndroidApp.NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelNameJava = new Java.Lang.String(channelName);
                var channel = new NotificationChannel(channelId, channelNameJava, NotificationImportance.Default)
                {
                    Description = channelDescription
                };
                manager.CreateNotificationChannel(channel);
            }

            channelInitialized = true;
        }

        public void SendLocalNotification(string title, string body)
        {
            if (!channelInitialized)
            {
                CreateNotificationChannel();
            }

            messageId++;

            Intent intent = new Intent(AndroidApp.Context, typeof(MainActivity));
            intent.PutExtra(TitleKey, title);
            intent.PutExtra(MessageKey, body);

            PendingIntent pendingIntent = PendingIntent.GetActivity(AndroidApp.Context, pendingIntentId, intent, PendingIntentFlags.OneShot);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(AndroidApp.Context, channelId)
                .SetContentIntent(pendingIntent)
                .SetContentTitle(title)
                .SetContentText(body)
                .SetLargeIcon(BitmapFactory.DecodeResource(AndroidApp.Context.Resources, Resource.Drawable.xamarin_logo))
                .SetSmallIcon(Resource.Drawable.xamarin_logo)
                .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate);

            var notification = builder.Build();
            manager.Notify(messageId, notification);

        }
    }
}