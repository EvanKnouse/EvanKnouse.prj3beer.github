using prj3beer.Models;
using prj3beer.Services;
using NUnit.Framework;

namespace nUnitTests
{
    class NotificationHandlerTests
    {
        Preference mockPref;

        [SetUp]
        public void SetUp()
        {
            mockPref = new Preference
            {
                Temperature = 0.0
            };
        }

        #region story16 Unit Tests
        [Test]
        public void TestThatAppDisplaysNotificationWhenReceivedTempMatchesDesiredTemp()
        {
            MockTempReadings.StartCounting(0.0, true, false);

            Assert.IsTrue(nh.LastNotification == NotificationType.PERFECT);
            Assert.IsTrue(nh.NotificationSent);
        }

        [Test]
        public void TestThatAppDisplaysNotificationWhenReceivedTempIsTwoDegreesAboveDesiredTemp()
        {

        }

        [Test]
        public void TestThatAppDisplaysNotificationWhenReceivedTempIsTwoDegreesBelowDesiredTemp()
        {

        }

        [Test]
        public void TestThatAppDisplaysGettingTooColdNotification()
        {

        }

        [Test]
        public void TestThatAppDisplaysGettingTooHotNotification()
        {

        }
        #endregion
    }
}
