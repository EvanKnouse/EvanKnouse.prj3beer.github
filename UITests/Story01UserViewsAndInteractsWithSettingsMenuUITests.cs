using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Story01UserViewsAndInteractsWithSettingsMenuUITests
    {
        IApp app;
        Platform platform;

        public Story01UserViewsAndInteractsWithSettingsMenuUITests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            //Initialize the app, arrive at home page (default for now)
            app = ConfigureApp.Android.ApkFile(@"D:\virpc\prj3beer\prj3.beer\prj3beer.Android\bin\Debug\com.companyname.prj3beer.apk").StartApp();
            //Tap into the screen navigation menu (default for now)
            app.Tap(c=>c.Button("ScreenSelectButton"));
        }

        [Test]
        public void TestTemperatureBelowRangeError()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("CurrentTempBox"));
            app.Screenshot("Status screen.");

            Assert.Equals(results[0].Text, "Temperature reading outside of range");
        }

        [Test]
        public void TestTemperatureAboveRangeError()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("currentTemp"));
            app.Screenshot("Status screen.");

            Assert.Equals(results[0].Text, "Temperature reading outside of range");
        }

        [Test]
        public void TestDeviceNotFoundError()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("currentTemp"));
            app.Screenshot("Status screen.");

            Assert.Equals(results[0].Text, "Waiting for device");
        }

        [Test]
        public void TestTemperatureInRangeAtMinusOne()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("currentTemp"));
            app.Screenshot("Status screen.");
            Assert.Equals(results[0].Text, "-1\u00B0C");
        }

        [Test]
        public void TestSettingsMenuIsDisplayedOnStatusScreenWhenSettingsButtonIsPressed()
        {
            app.Tap(c => c.Button("SettingsMenuButton"));
        }

        [Test]
        public void TestSettingsMenuIsDisplayedOnSelectionScreenWhenSettingsButtonIsPressed()
        {
            app.Tap(c => c.Button("SettingsMenuButton"));
        }

        [Test]
        public void TestSettingsAreAppliedOnSettingsChange()
        {
            app.Tap(c => c.Button("SettingsMenuButton"));
            app.Tap(c => c.Button("TemperatureSettingsButton"));
        }

        [Test]
        public void TestThatSettingsMenuDisplaysCurrentAppSettings()
        {

        }
    }
}
