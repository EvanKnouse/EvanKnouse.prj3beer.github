using NUnit.Framework;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using prj3beer.Models;
using Xamarin.Forms;
using Xamarin.UITest.Configuration;
using prj3beer.Services;

namespace UITests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    class SettingsTests
    {
        IApp app;
        Platform platform;

        string apkFile = "D:\\virpc\\prj3beer\\prj3.beer\\prj3beer\\prj3beer.Android\\bin\\Debug\\com.companyname.prj3beer.apk";

        public SettingsTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            //Initialize the app, arrive at home page (default for now)
            app = ConfigureApp.Android.ApkFile(apkFile).StartApp();

            ////Tap into the screen navigation menu (default for now)
            //app.Tap(c => c.Marked("ScreenSelectButton"));

            //Sets the Temperature settings to celsius for every test
            Settings.TemperatureSettings = true;

            //Sets the master Notification setting to on
            Settings.NotificationSettings = true;

            //Sets the In Range Notification setting to on
            Settings.InRangeSettings = true;

            //Sets the Too Hot/Cold Notification setting to on
            Settings.NotificationSettings = true;

            //Tap into the screen navigation menu
            app.TapCoordinates(1350, 175);
        }

        [Test]
        public void TestSettingsMenuIsDisplayedOnStatusScreenWhenSettingsButtonIsPressed()
        {
            //Pick Status screen from the screen selection menu
            //app.Tap("Status");

            //Wait for the Settings button to appear on screen
            app.WaitForElement("Settings");

            //Press Settings Menu button
            app.Tap("Settings");

            //Wait for the Temperature switch to appear on screen
            app.WaitForElement("SettingsTable");

            //Look for the toggle button on the Settings Menu
            AppResult[] button = app.Query(("SettingsTable"));

            //Will be greater than 0 if it exists, returns AppResult[]
            Assert.IsTrue(button.Any());
        }

        #region Story16 Tests
        [Test]
        public void TestThatAppDisplaysNotificationWhenReceivedTempMatchesDesiredTemp()
        {
            //The notification sent should be PERFECT (3)
            Assert.AreEqual(Notifications.TryNotification(5, 5, NotificationType.NO_MESSAGE), 3);
        }

        [Test]
        public void TestThatAppDisplaysNotificationWhenReceivedTempIsTwoDegreesAboveDesiredTemp()
        {
            //The notification sent should be IN_RANGE_HOT (2)
            Assert.AreEqual(Notifications.TryNotification(7, 5, NotificationType.TOO_HOT), 2);
        }

        [Test]
        public void TestThatAppDisplaysNotificationWhenReceivedTempIsTwoDegreesBelowDesiredTemp()
        {
            //The notification sent should be IN_RANGE_COLD (4)
            Assert.AreEqual(Notifications.TryNotification(3, 5, NotificationType.NO_MESSAGE), 4);
        }

        [Test]
        public void TestThatAppDisplaysGettingTooColdNotification()
        {
            //The notification sent should be a TOO_COLD notification (5)
            Assert.AreEqual(Notifications.TryNotification(0, 5, NotificationType.PERFECT), 5);
        }

        [Test]
        public void TestThatAppDisplaysGettingTooHotNotification()
        {
            //The notification sent should be a TOO_HOT notification (1)
            Assert.AreEqual(Notifications.TryNotification(10, 5, NotificationType.PERFECT), 1);
        }
        #endregion

        #region Story 15 UI Tests
        [Test]
        public void TestThatTurningOffMasterNotificationSwitchHidesNotificationsSubSettings()
        {
            //Pick status screen from the screen selection menu
            //app.Tap("Status");

            //Wait for the Settings button to appear on screen
            app.WaitForElement("Settings");

            //Press Settings menu button
            app.Tap("Settings");

            app.WaitForElement("SettingsTable");

            //Look for InRange switch
            AppResult[] results = app.Query(e => e.Class("SwitchCellView").Class("Switch").Index(2));

            //Does the InRange switch exist before turning off master notifications
            Assert.IsTrue(results.Any());

            //Tap on the master notification switch, turning notifications off
            app.Tap(e => e.Class("SwitchCellView").Class("Switch").Index(1));

            //Get the result of querying for the notification sub-setting switch
            results = app.Query(e => e.Class("SwitchCellView").Class("Switch").Index(2));

            //Results should not contain the notification in range switch
            Assert.IsFalse(results.Any());

        }

        [Test]
        public void TestThatTurningOnMasterNotificationSwitchShowsNotificationsSubSettings()
        {
            //Pick status screen from the screen selection menu
            //app.Tap("Status");

            //Wait for the Settings button to appear on screen
            app.WaitForElement("Settings");

            //Press Settings menu button
            app.Tap("Settings");

            app.WaitForElement("SettingsTable");

            //Turn off master notifications
            app.Tap(e => e.Class("SwitchCellView").Class("Switch").Index(1));

            //Get the result of querying for the inRange notification switch
            AppResult[] results = app.Query(e => e.Class("SwitchCellView").Class("Switch").Index(2));

            //Results should not contain the inRange switch
            Assert.IsFalse(results.Any());

            //app.WaitForElement("notificationSubSettings");

            //Turn on notifications by tapping master notification switch
            app.Tap(e => e.Class("SwitchCellView").Class("Switch").Index(1));

            //Get the result of querying for the inRange notification switch
            results = app.Query(e => e.Class("SwitchCellView").Class("Switch").Index(2));

            //Results should contain the notification in range switch
            Assert.IsTrue(results.Any());
        }

        [Test]
        public void TestThatNotificationSettingsPersistOnAppReload()
        {
            //Pick Status screen from the screen selection menu
            //app.Tap("Status");

            app.WaitForElement("Settings");

            //Press Settings menu button
            app.Tap("Settings");

            app.WaitForElement("SettingsTable");

            //Tap the master notifications switch
            app.Tap(e => e.Class("SwitchCellView").Class("Switch").Index(1));

            bool firstSwitchValue = app.Query(e => e.Class("SwitchCellView").Class("Switch").Index(1).Invoke("isChecked").Value<bool>()).First();

            app = ConfigureApp.Android.ApkFile(apkFile).StartApp(AppDataMode.DoNotClear);

            //Tap into the screen navigation menu
            app.TapCoordinates(1350, 175);

            app.WaitForElement("Settings");

            //Press Settings menu button
            app.Tap("Settings");

            app.WaitForElement("SettingsTable");

            bool secondSwitchValue = app.Query(e => e.Class("SwitchCellView").Class("Switch").Index(1).Invoke("isChecked").Value<bool>()).First();

            Assert.AreEqual(true, firstSwitchValue == secondSwitchValue);
        }

        [Test]
        public void TestThatNoNotificationsSentIfSetToOff()
        {
            //Set master notifications setting to off
            Settings.NotificationSettings = false;

            Assert.AreEqual(0, Notifications.TryNotification(6, 5, NotificationType.NO_MESSAGE));

            Assert.AreEqual(0, Notifications.TryNotification(5, 5, NotificationType.NO_MESSAGE));

            Assert.AreEqual(0, Notifications.TryNotification(0, 5, NotificationType.NO_MESSAGE));
        }

        [Test]
        public void TestThatAllNotificationsAreSentWhenAllNotificationsAreOn()
        {
            //Set master notifications setting to on
            Settings.NotificationSettings = true;

            //Set in range notifications setting to on
            Settings.InRangeSettings = true;

            //Set too hot/cold notifications setting to on
            Settings.TooHotColdSettings = true;

            Assert.AreEqual(Notifications.TryNotification(6, 5, NotificationType.NO_MESSAGE), 2);

            Assert.AreEqual(Notifications.TryNotification(5, 5, NotificationType.IN_RANGE_HOT), 3);

            Assert.AreEqual(Notifications.TryNotification(0, 5, NotificationType.PERFECT), 5);
        }

        [Test]
        public void TestThatOnlyPerfectNotificationsAreSent()
        {
            //Set master notifications setting is on
            Settings.NotificationSettings = true;

            //Set in range notifications setting to off
            Settings.InRangeSettings = false;

            //Set too hot/cold notifications setting to off
            Settings.TooHotColdSettings = false;

            Assert.AreEqual(0, Notifications.TryNotification(6, 5, NotificationType.NO_MESSAGE));

            Assert.AreEqual(3, Notifications.TryNotification(5, 5, NotificationType.NO_MESSAGE));

            Assert.AreEqual(0, Notifications.TryNotification(0, 5, NotificationType.PERFECT));
        }

        [Test]
        public void TestThatOnlyPerfectAndInRangeNotificationsAreSent()
        {
            //Set master notifications setting is on
            Settings.NotificationSettings = true;

            //Set in range notifications setting to on
            Settings.InRangeSettings = true;

            //Set too hot/cold notifications setting to off
            Settings.TooHotColdSettings = false;

            Assert.AreEqual(Notifications.TryNotification(6, 5, NotificationType.NO_MESSAGE), 2);

            Assert.AreEqual(Notifications.TryNotification(5, 5, NotificationType.IN_RANGE_HOT), 3);

            Assert.AreEqual(Notifications.TryNotification(0, 5, NotificationType.PERFECT), 0);
        }

        [Test]
        public void TestThatOnlyPerfectAndTooHotColdNotificationsAreSent()
        {
            //Set master notifications setting is on
            Settings.NotificationSettings = true;

            //Set in range notifications setting to off
            Settings.InRangeSettings = false;

            //Set too hot/cold notifications setting to on
            Settings.TooHotColdSettings = true;

            Assert.AreEqual(Notifications.TryNotification(6, 5, NotificationType.NO_MESSAGE), 0);

            Assert.AreEqual(Notifications.TryNotification(5, 5, NotificationType.NO_MESSAGE), 3);

            Assert.AreEqual(Notifications.TryNotification(0, 5, NotificationType.PERFECT), 5);
        }
        #endregion
    }
}
