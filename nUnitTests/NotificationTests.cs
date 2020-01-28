using prj3beer.Services;
using prj3beer.Models;
using NUnit.Framework;

namespace nUnitTests
{
    class NotificationTests
    {

        #region story16 Unit Tests
        [Test]
        public void TestThatAppDisplaysNotificationWhenReceivedTempMatchesDesiredTemp()
        {
            //MockTempReadings.StartCounting(0.0, true, false);
            //Thread.Sleep(1000);
            //Assert.IsTrue(nh.LastNotification == NotificationType.PERFECT);
            //Assert.IsTrue(nh.NotificationSent);

            Assert.AreEqual(Notifications.TryNotification(5, 5, NotificationType.NO_MESSAGE), 3);
        }

        [Test]
        public void TestThatAppDisplaysNotificationWhenReceivedTempIsTwoDegreesAboveDesiredTemp()
        {
            
            //MockTempReadings.StartCounting(2.0, true, false);
            //Thread.Sleep(1000);
            //Assert.IsTrue(nh.LastNotification == NotificationType.IN_RANGE_HOT);
            //Assert.IsTrue(nh.NotificationSent);
            
            Assert.AreEqual(Notifications.TryNotification(7, 5, NotificationType.NO_MESSAGE), 2);
        }

        [Test]
        public void TestThatAppDisplaysNotificationWhenReceivedTempIsTwoDegreesBelowDesiredTemp()
        {

            //MockTempReadings.StartCounting(-2.0, true, false);
            //Thread.Sleep(1000);
            //Assert.IsTrue(nh.LastNotification == NotificationType.IN_RANGE_COLD);
            //Assert.IsTrue(nh.NotificationSent);
            Assert.AreEqual(Notifications.TryNotification(3, 5, NotificationType.NO_MESSAGE), 4);
        }

        [Test]
        public void TestThatAppDisplaysGettingTooColdNotification()
        {
            //MockTempReadings.StartCounting(10.0, true, false);
            //Thread.Sleep(1000);
            //MockTempReadings.StartCounting(-5.0, true, false);
            //Thread.Sleep(1000);
            //Assert.IsTrue(nh.LastNotification == NotificationType.TOO_COLD);
            //Assert.IsTrue(nh.NotificationSent);
            Assert.AreEqual(Notifications.TryNotification(0, 5, NotificationType.PERFECT), 5);
        }

        [Test]
        public void TestThatAppDisplaysGettingTooHotNotification()
        {
            //MockTempReadings.StartCounting(-10.0, true, false);
            //Thread.Sleep(1000);
            //MockTempReadings.StartCounting(5.0, true, false);
            //Thread.Sleep(1000);
            //Assert.IsTrue(nh.LastNotification == NotificationType.TOO_HOT);
            //Assert.IsTrue(nh.NotificationSent);
            Assert.AreEqual(Notifications.TryNotification(10, 5, NotificationType.PERFECT), 1);
        }

        [Test]
        public void AppDoesNotSendTwoPerfectTemperatureNotificationsInARow(){
            //TestThatAppDisplaysNotificationWhenReceivedTempMatchesDesiredTemp();
            //Thread.Sleep(1000);
            //Assert.IsTrue(nh.LastNotification == NotificationType.PERFECT);
            //Assert.IsFalse(nh.NotificationSent);
            Assert.AreEqual(Notifications.TryNotification(5, 5, NotificationType.PERFECT), 0);
        }

        [Test]
        public void AppDoesNotSendTwoTemperatureGettingInWarmRangeNotifications(){
            //TestThatAppDisplaysNotificationWhenReceivedTempIsTwoDegreesAboveDesiredTemp();
            //Thread.Sleep(1000);
            //Assert.IsTrue(nh.LastNotification == NotificationType.IN_RANGE_HOT);
            //Assert.IsFalse(nh.NotificationSent);
            Assert.AreEqual(Notifications.TryNotification(7, 5, NotificationType.IN_RANGE_HOT), 0);
        }

        [Test]
        public void AppDoesNotSendTwoTemperatureGettingInColdRangeNotifications(){
            //TestThatAppDisplaysNotificationWhenReceivedTempIsTwoDegreesBelowDesiredTemp();
            //Thread.Sleep(1000);
            //Assert.IsTrue(nh.LastNotification == NotificationType.IN_RANGE_COLD);
            //Assert.IsFalse(nh.NotificationSent);
            Assert.AreEqual(Notifications.TryNotification(3, 5, NotificationType.IN_RANGE_COLD), 0);
        }

        [Test]
        public void AppDoesNotSendTwoTemeratureGettingTooHotNotificationsInARow(){
            //TestThatAppDisplaysGettingTooHotNotification();
            //Thread.Sleep(1000);
            //Assert.IsTrue(nh.LastNotification == NotificationType.TOO_HOT);
            //Assert.IsFalse(nh.NotificationSent);
            Assert.AreEqual(Notifications.TryNotification(10, 5, NotificationType.TOO_HOT), 0);
        }

        [Test]
        public void AppDoesNotSendTwoTemeratureGettingTooColdNotificationsInARow()
        {
            //TestThatAppDisplaysGettingTooColdNotification();
            //Thread.Sleep(1000);
            //Assert.IsTrue(nh.LastNotification == NotificationType.TOO_COLD);
            //Assert.IsFalse(nh.NotificationSent);
            Assert.AreEqual(Notifications.TryNotification(0, 5, NotificationType.TOO_COLD), 0);
        }

        [Test]
        public void AppDoesNotSendGettingInRangeWarmNotificationIfLastNotificationSetWasPerfect()
        {
            //TestThatAppDisplaysNotificationWhenReceivedTempMatchesDesiredTemp();
            //Thread.Sleep(1000);
            //MockTempReadings.StartCounting(1, true, false);
            //Thread.Sleep(1000);
            //Assert.IsTrue(nh.LastNotification == NotificationType.PERFECT);
            //Assert.IsFalse(nh.NotificationSent);
            Assert.AreEqual(Notifications.TryNotification(7, 5, NotificationType.PERFECT), 0);
        }

        [Test]
        public void AppDoesNotSendGettingInRangeColdNotificationIfLastNotificationSetWasPerfect()
        {
            //TestThatAppDisplaysNotificationWhenReceivedTempMatchesDesiredTemp();
            //Thread.Sleep(1000);
            //MockTempReadings.StartCounting(-1, true, false);
            //Thread.Sleep(1000);
            //Assert.IsTrue(nh.LastNotification == NotificationType.PERFECT);
            //Assert.IsFalse(nh.NotificationSent);
            Assert.AreEqual(Notifications.TryNotification(3, 5, NotificationType.PERFECT), 0);
        }

        #endregion

        #region story15 Unit Tests
        [Test]
        public void TestThatNoNotificationsAreSentIfNotificationsSetToOff()
        {
            Settings.NotificationSettings = false;

            Assert.AreEqual(0, Notifications.TryNotification(6, 5, NotificationType.NO_MESSAGE));

            Assert.AreEqual(0, Notifications.TryNotification(5, 5, NotificationType.NO_MESSAGE));

            Assert.AreEqual(0, Notifications.TryNotification(0, 5, NotificationType.NO_MESSAGE));
        }

        [Test]
        public void TestThatAllNotificationsAreSentWhenAllNotificationsAreOn()
        {
            Settings.NotificationSettings = true;
            Settings.InRangeSettings = true;
            Settings.TooHotColdSettings = true;

            Assert.AreEqual(Notifications.TryNotification(6, 5, NotificationType.NO_MESSAGE), 2);

            Assert.AreEqual(Notifications.TryNotification(5, 5, NotificationType.IN_RANGE_HOT), 3);

            Assert.AreEqual(Notifications.TryNotification(0, 5, NotificationType.PERFECT), 5);
        }

        [Test]
        public void TestThatOnlyPerfectAndTooHotColdNotificationsAreSent()
        {
            Settings.NotificationSettings = true;
            Settings.InRangeSettings = false;
            Settings.TooHotColdSettings = true;

            Assert.AreEqual(Notifications.TryNotification(6, 5, NotificationType.NO_MESSAGE), 0);

            Assert.AreEqual(Notifications.TryNotification(5, 5, NotificationType.NO_MESSAGE), 3);

            Assert.AreEqual(Notifications.TryNotification(0, 5, NotificationType.PERFECT), 5);
        }

        [Test]
        public void TestThatOnlyPerfectAndInRangeNotificationsAreSent()
        {
            Settings.NotificationSettings = true;
            Settings.InRangeSettings = true;
            Settings.TooHotColdSettings = false;

            Assert.AreEqual(Notifications.TryNotification(6, 5, NotificationType.NO_MESSAGE), 2);

            Assert.AreEqual(Notifications.TryNotification(5, 5, NotificationType.IN_RANGE_HOT), 3);

            Assert.AreEqual(Notifications.TryNotification(0, 5, NotificationType.PERFECT), 0);
        }

        [Test]
        public void TestThatOnlyPerfectNotificationsAreSent()
        {
            Settings.NotificationSettings = true;
            Settings.InRangeSettings = false;
            Settings.TooHotColdSettings = false;

            Assert.AreEqual(Notifications.TryNotification(6, 5, NotificationType.NO_MESSAGE), 0);

            Assert.AreEqual(Notifications.TryNotification(5, 5, NotificationType.NO_MESSAGE), 3);

            Assert.AreEqual(Notifications.TryNotification(0, 5, NotificationType.PERFECT), 0);
        }
        #endregion
    }
}
