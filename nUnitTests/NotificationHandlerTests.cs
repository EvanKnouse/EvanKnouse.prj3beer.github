using prj3beer.Models;
using prj3beer.Services;
using NUnit.Framework;
using System.Threading;
using Xamarin.Forms;
using Microsoft.DotNet.PlatformAbstractions;

namespace nUnitTests
{
    class NotificationHandlerTests
    {
        Preference mockPref;
        INotificationHandler nh;


        [SetUp]
        public void SetUp()
        {
            mockPref = new Preference
            {
                Temperature = 0.0
            };

            
            nh = DependencyService.Get<INotificationHandler>();
            

        }

        #region story16 Unit Tests
        [Test]
        public void TestThatAppDisplaysNotificationWhenReceivedTempMatchesDesiredTemp()
        {
            MockTempReadings.StartCounting(0.0, true, false);
            Thread.Sleep(1000);
            Assert.IsTrue(nh.LastNotification == NotificationType.PERFECT);
            
            Assert.IsTrue(nh.NotificationSent);
        }

        [Test]
        public void TestThatAppDisplaysNotificationWhenReceivedTempIsTwoDegreesAboveDesiredTemp()
        {
            MockTempReadings.StartCounting(2.0, true, false);
            Thread.Sleep(1000);
            Assert.IsTrue(nh.LastNotification == NotificationType.IN_RANGE_HOT);
            Assert.IsTrue(nh.NotificationSent);
        }

        [Test]
        public void TestThatAppDisplaysNotificationWhenReceivedTempIsTwoDegreesBelowDesiredTemp()
        {
            MockTempReadings.StartCounting(-2.0, true, false);
            Thread.Sleep(1000);
            Assert.IsTrue(nh.LastNotification == NotificationType.IN_RANGE_COLD);
            Assert.IsTrue(nh.NotificationSent);
        }

        [Test]
        public void TestThatAppDisplaysGettingTooColdNotification()
        {
            MockTempReadings.StartCounting(10.0, true, false);
            Thread.Sleep(1000);
            MockTempReadings.StartCounting(-5.0, true, false);
            Thread.Sleep(1000);
            Assert.IsTrue(nh.LastNotification == NotificationType.TOO_COLD);
            Assert.IsTrue(nh.NotificationSent);
        }

        [Test]
        public void TestThatAppDisplaysGettingTooHotNotification()
        {
            MockTempReadings.StartCounting(-10.0, true, false);
            Thread.Sleep(1000);
            MockTempReadings.StartCounting(5.0, true, false);
            Thread.Sleep(1000);
            Assert.IsTrue(nh.LastNotification == NotificationType.TOO_HOT);
            Assert.IsTrue(nh.NotificationSent);
        }

        [Test]
        public void AppDoesNotSendTwoPerfectTemperatureNotificationsInARow(){
            TestThatAppDisplaysNotificationWhenReceivedTempMatchesDesiredTemp();
            Thread.Sleep(1000);
            Assert.IsTrue(nh.LastNotification == NotificationType.PERFECT);
            Assert.IsFalse(nh.NotificationSent);
        }

        [Test]
        public void AppDoesNotSendTwoTemperatureGettingInWarmRangeNotifications(){
            TestThatAppDisplaysNotificationWhenReceivedTempIsTwoDegreesAboveDesiredTemp();
            Thread.Sleep(1000);
            Assert.IsTrue(nh.LastNotification == NotificationType.IN_RANGE_HOT);
            Assert.IsFalse(nh.NotificationSent);
        }

        [Test]
        public void AppDoesNotSendTwoTemperatureGettingInColdRangeNotifications(){
            TestThatAppDisplaysNotificationWhenReceivedTempIsTwoDegreesBelowDesiredTemp();
            Thread.Sleep(1000);
            Assert.IsTrue(nh.LastNotification == NotificationType.IN_RANGE_COLD);
            Assert.IsFalse(nh.NotificationSent);
        }

        [Test]
        public void AppDoesNotSendTwoTemeratureGettingTooHotNotificationsInARow(){
            TestThatAppDisplaysGettingTooHotNotification();
            Thread.Sleep(1000);
            Assert.IsTrue(nh.LastNotification == NotificationType.TOO_HOT);
            Assert.IsFalse(nh.NotificationSent);
        }

        [Test]
        public void AppDoesNotSendTwoTemeratureGettingTooColdNotificationsInARow()
        {
            TestThatAppDisplaysGettingTooColdNotification();
            Thread.Sleep(1000);
            Assert.IsTrue(nh.LastNotification == NotificationType.TOO_COLD);
            Assert.IsFalse(nh.NotificationSent);
        }

        [Test]
        public void AppDoesNotSendGettingInRangeWarmNotificationIfLastNotificationSetWasPerfect()
        {
            TestThatAppDisplaysNotificationWhenReceivedTempMatchesDesiredTemp();
            Thread.Sleep(1000);
            MockTempReadings.StartCounting(1, true, false);
            Thread.Sleep(1000);
            Assert.IsTrue(nh.LastNotification == NotificationType.PERFECT);
            Assert.IsFalse(nh.NotificationSent);
        }

        [Test]
        public void AppDoesNotSendGettingInRangeColdNotificationIfLastNotificationSetWasPerfect()
        {
            TestThatAppDisplaysNotificationWhenReceivedTempMatchesDesiredTemp();
            Thread.Sleep(1000);
            MockTempReadings.StartCounting(-1, true, false);
            Thread.Sleep(1000);
            Assert.IsTrue(nh.LastNotification == NotificationType.PERFECT);
            Assert.IsFalse(nh.NotificationSent);
        }

        #endregion
    }
}
