using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Xamarin.Forms;

namespace UITests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Story01UserViewsAndInteractsWithSettingsMenuUITests
    {
        IApp app;
        Platform platform;
        string apkFile = "D:\\virpc\\prj3beer\\prj3.beer\\prj3beer.Android\\bin\\Debug\\com.companyname.prj3beer.apk";

        public Story01UserViewsAndInteractsWithSettingsMenuUITests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            //Initialize the app, arrive at home page (default for now)
            app = ConfigureApp.Android.ApkFile(apkFile).StartApp();
            //Tap into the screen navigation menu
            app.TapCoordinates(150, 90);
            ////Tap into the screen navigation menu (default for now)
            //app.Tap("ScreenSelectButton");
        }

        [Test]
        public void TestTemperatureBelowRangeError()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("CurrentTempBox"));
            app.Screenshot("Status");

            Assert.Equals(results[0].Text, "Temperature reading outside of range");
        }

        [Test]
        public void TestTemperatureAboveRangeError()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("currentTemp"));
            app.Screenshot("Status");

            Assert.Equals(results[0].Text, "Temperature reading outside of range");
        }

        [Test]
        public void TestDeviceNotFoundError()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("currentTemp"));
            app.Screenshot("Status");

            Assert.Equals(results[0].Text, "Waiting for device");
        }

        [Test]
        public void TestTemperatureInRangeAtMinusOne()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("currentTemp"));
            app.Screenshot("Status");
            Assert.Equals(results[0].Text, "-1\u00B0C");
        }

        [Test]
        public void TestSettingsMenuIsDisplayedOnStatusScreenWhenSettingsButtonIsPressed()
        {
            //Pick Status screen from the screen selection menu
            app.Tap("Status");
            //Press Settings Menu button
            app.Tap("SettingsMenuButton");
            //Look for the toggle button on the Settings Menu
            AppResult[] button = app.Query(("temperatureToggle"));
            //Will be greater than 0 if it exists, returns AppResult[]
            Assert.IsTrue(button.Any());
        }

        [Test]
        public void TestSettingsMenuIsDisplayedOnSelectionScreenWhenSettingsButtonIsPressed()
        {
            //Pick Status screen from the screen selection menu
            app.Tap("Selection");
            //Press Settings Menu button
            app.Tap("SettingsMenuButton");
            //Look for the toggle button on the Settings Menu
            AppResult[] temperatureSwitch = app.Query(c=>c.Id("temperatureToggle"));
            //Will be greater than 0 if it exists, returns AppResult[]
            Assert.IsTrue(temperatureSwitch.Any());
        }

        [Test]
        public void TestSettingsAreAppliedOnSettingsChange()
        {
            //Pick Status screen from the screen selection menu
            app.Tap("Status");
            //Press Settings menu button
            app.Tap("SettingsMenuButton");
            //Tap on the toggle button to change the temperature setting to fahrenheit
            app.Tap("temperatureToggle");
            //Go back to the Status screen
            app.Back();
            //Check that the label for the current temperature is set to "\u00B0F"
            string tempLabel = app.Query(c=>c.Id("currentTempLabel"))[0].Text;

            Assert.AreEqual("\u00B0F", tempLabel);
        }

        [Test]
        public void TestThatSettingsMenuDisplaysCurrentAppSettings()
        {
            //Pick status screen from the screen selection menu
            app.Tap("Status");
            //Press Settings menu button
            app.Tap("SettingsMenuButton");
            //Tap on the toggle button to change the temperature setting to fahrenheit 
            app.Tap("fahrenheitEnabled");
            //Go back to the Status screen
            app.Back();

            //Go back to the Settings menu button
            app.Tap("SettingsMenuButton");
            bool toggled = app.Query(c => c.Id("fahrenheitEnabled"))[0].Enabled;

            Switch testSwitch = new Switch();
            testSwitch.IsToggled = toggled;

            Assert.AreEqual(testSwitch.IsToggled, true);
        }
    }
}
