using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void TemperatureBelowRange()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("currentTemp"));
            app.Screenshot("Status screen.");

            Assert.Equals(results[0].Text, "Temperature reading outside of range");
        }

        [Test]
        public void TemperatureAboveRange()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("currentTemp"));
            app.Screenshot("Status screen.");

            Assert.Equals(results[0].Text, "Temperature reading outside of range");
        }

        [Test]
        public void DeviceNotFound()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("currentTemp"));
            app.Screenshot("Status screen.");

            Assert.Equals(results[0].Text, "Waiting for device");
        }

        [Test]
        public void TemperatureInRangeAtMinusOne()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("currentTemp"));
            app.Screenshot("Status screen.");
            Assert.Equals(results[0].Text, "-1\u00B0C");

        }
    }
}
