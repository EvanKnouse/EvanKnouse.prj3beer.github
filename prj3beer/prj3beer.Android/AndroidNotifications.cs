using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;

using prj3beer.Services;
using Xamarin.Forms;
using AndroidApp = Android.App.Application;

[assembly: Dependency(typeof(prj3beer.Droid.AndroidNotifications))]
namespace prj3beer.Droid
{
    class AndroidNotifications : INotificationHandler
    {

        const string channelId = "temperature";
        const string channelName = "TemperatureNotifications";
        const string channelDescription = "The channel for notifications specific to drink temperature.";

        int messageId = -1; //incrementing notification id to distinguish unique notifications
        NotificationManager manager;

        bool channelInitialized = false;

        /// <summary>
        /// Part of the INotificationHandler Interface, calls Android-specific notification-setup methods.
        /// </summary>
        public void Initialize()
        {
            CreateNotificationChannel();
        }

        /// <summary>
        /// 
        /// </summary>
        void CreateNotificationChannel()
        {
            //Create a notification manager
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

        /// <summary>
        /// Part of the INoficationHandler Interface.  Sends a notification with Android-specific code.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="body"></param>
        public void SendLocalNotification(string title, string body)
        {
            if (!channelInitialized)
            {
                CreateNotificationChannel();
            }

            messageId++; //increment notificationID to guarantee unique ID

            NotificationCompat.Builder builder = new NotificationCompat.Builder(AndroidApp.Context, channelId)
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