using prj3beer.Services;
using prj3beer.Models;
using NUnit.Framework;
using Plugin.Settings;

namespace nUnitTests
{
    /// <summary>
    /// These tests are responsible for testing the app's notification functionality
    /// </summary>
    class NotificationTests
    { 
        [Test]
        public void AppDoesNotSendTwoPerfectTemperatureNotificationsInARow()
        {
            //TestThatAppDisplaysNotificationWhenReceivedTempMatchesDesiredTemp();
            //Thread.Sleep(1000);
            //Assert.IsTrue(nh.LastNotification == NotificationType.PERFECT);
            //Assert.IsFalse(nh.NotificationSent);
            Assert.AreEqual(Notifications.TryNotification(5, 5, NotificationType.PERFECT), 0);
        }

        [Test]
        public void AppDoesNotSendTwoTemperatureGettingInWarmRangeNotifications()
        {
            //TestThatAppDisplaysNotificationWhenReceivedTempIsTwoDegreesAboveDesiredTemp();
            //Thread.Sleep(1000);
            //Assert.IsTrue(nh.LastNotification == NotificationType.IN_RANGE_HOT);
            //Assert.IsFalse(nh.NotificationSent);
            Assert.AreEqual(Notifications.TryNotification(7, 5, NotificationType.IN_RANGE_HOT), 0);
        }

        [Test]
        public void AppDoesNotSendTwoTemperatureGettingInColdRangeNotifications()
        {
            //TestThatAppDisplaysNotificationWhenReceivedTempIsTwoDegreesBelowDesiredTemp();
            //Thread.Sleep(1000);
            //Assert.IsTrue(nh.LastNotification == NotificationType.IN_RANGE_COLD);
            //Assert.IsFalse(nh.NotificationSent);
            Assert.AreEqual(Notifications.TryNotification(3, 5, NotificationType.IN_RANGE_COLD), 0);
        }

        [Test]
        public void AppDoesNotSendTwoTemperatureGettingTooHotNotificationsInARow()
        {
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
    }
}
