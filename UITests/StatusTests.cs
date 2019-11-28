using NUnit.Framework;
using System.Linq;
using Xamarin.Forms;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class StatusTests
    {
        IApp app;
        Platform platform;
        string apkFile = "D:\\virpc\\prj3beer\\prj3.beer\\prj3beer.Android\\bin\\Debug\\com.companyname.prj3beer.apk";

        public StatusTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            //Initialize the app, arrive at home page (default for now)
            app = ConfigureApp.Android.ApkFile(apkFile).StartApp();
            //Tap into the screen navigation menu
            //app.TapCoordinates(150, 90);
            ////Tap into the screen navigation menu (default for now)
            app.Tap(c => c.Marked("ScreenSelectButton"));
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
            string tempLabel = app.Query(c => c.Id("currentTempLabel"))[0].Text;

            //If equal, the temperature label has been set to fahrenheit and the settings have been applied
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
