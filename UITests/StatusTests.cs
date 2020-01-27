using NUnit.Framework;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using prj3beer.Models;

namespace UITests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class StatusTests
    {
        IApp app;
        Platform platform;

        string apkFile = "D:\\virpc\\prj3beer\\prj3.beer\\prj3beer\\prj3beer.Android\\bin\\Debug\\com.companyname.prj3beer.apk";

        public StatusTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            //Initialize the app, arrive at home page (default for now)
            app = app = ConfigureApp.Android.ApkFile(apkFile).StartApp();
            //Tap into the screen navigation menu
            app.TapCoordinates(150, 90);
            ////Tap into the screen navigation menu (default for now)
            //app.Tap(c => c.Marked("ScreenSelectButton"));

            //Sets the Temperature settings to celcius everytest
            Settings.TemperatureSettings = true;
        }

        [Test]
        public void TestSettingsMenuIsDisplayedOnStatusScreenWhenSettingsButtonIsPressed()
        {
            //Pick Status screen from the screen selection menu
            app.Tap("Status");

            //Wait for the Settings button to appear on screen
            app.WaitForElement("Settings");

            //Press Settings Menu button
            app.Tap("Settings");

            //Wait for the Temperature switch to appear on screen
            app.WaitForElement("switchTemp");

            //Look for the toggle button on the Settings Menu
            AppResult[] button = app.Query(("switchTemp"));

            //Will be greater than 0 if it exists, returns AppResult[]
            Assert.IsTrue(button.Any());
        }

        [Test]
        public void TestSettingsAreAppliedOnSettingsChange()
        {
            Settings.TemperatureSettings = true;

            //Pick Status screen from the screen selection menu
            app.Tap("Status");

            //Wait for the Settings button to appear on screen
            app.WaitForElement("Settings");

            //Press Settings menu button
            app.Tap("Settings");

            //Wait for the Temperature switch to appear on screen
            app.WaitForElement("switchTemp");

            //Check that the label for the current temperature is set to "\u00B0C"
            string tempLabel = app.Query("currentTemperature")[0].Text;

            //If equal, the temperature label has been set to Celsius
            Assert.AreEqual(tempLabel.Contains("\u00B0C"), true);

            //Tap on the toggle button to change the temperature setting to fahrenheit
            app.Tap("switchTemp");

            //Go back to the Status screen
            app.Back();

            //Wait for the Current Temperature Label to appear on screen
            app.WaitForElement("currentTemperature");

            // Wait for the temp to read 45F
            app.WaitForElement("45\u00B0F");

            //Check that the label for the current temperature is set to "\u00B0F"
            tempLabel = app.Query("currentTemperature")[0].Text;

            //If equal, the temperature label has been set to fahrenheit and the settings have been applied
            Assert.AreEqual(tempLabel.Contains("\u00B0F"), true);
        }

        [Test]
        public void TestThatSettingsMenuDisplaysCurrentAppSettings()
        {
            //Pick status screen from the screen selection menu
            app.Tap("Status");

            //Wait for the Settings button to appear on screen
            app.WaitForElement("Settings");

            //Press Settings menu button
            app.Tap("Settings");

            //Wait for the Temperature switch to appear on screen
            app.WaitForElement("switchTemp");

            //Grab the temperature label text to prove it was Celsius before it switched
            string toggled = app.Query("lblTemp")[0].Text;

            //If it was originally Celsius it would pass
            Assert.AreEqual(toggled, "Celsius");

            //Tap on the toggle button to change the temperature setting to fahrenheit
            app.Tap("switchTemp");

            //Grab the temperature label text to prove it switched
            toggled = app.Query("lblTemp")[0].Text;

            //Check if the enabled value is true
            Assert.AreEqual(toggled, "Fahrenheit");
        }

        #region Story 04 UI Tests
        // Test that the application is currently on the status page
        [Test]
        public void AppIsOnStatusPage()
        {
            //Pick Status screen from the screen selection menu
            //app.Tap("Status");
            //Thread.Sleep(5000);

            app.TapCoordinates(150, 90);

            //Thread.Sleep(5000);

            app.WaitForElement("StatusPage");

            AppResult[] results = app.Query("StatusPage");

            Assert.IsTrue(results.Any());
        }

        // Test that the target temperature entry field is on the status page
        [Test]
        public void TargetTempEntryIsOnPage()
        {
            //Pick Status screen from the screen selection menu
            app.Tap("Status");

            app.WaitForElement("currentTarget");

            AppResult[] results = app.Query("currentTarget");

            Assert.IsTrue(results.Any());
        }

        // Test that the target temperature stepper buttons are on the status page
        [Test]
        public void StepperIsOnPage()
        {
            //Pick Status screen from the screen selection menu
            app.Tap("Status");

            app.WaitForElement("TempStepper");

            AppResult[] results = app.Query("TempStepper");
            Assert.IsTrue(results.Any());
        }

        // Test that a temperature value can be entered in the entry field on the status page
        [Test]
        public void TargetTempEntryFieldCanBeSetManually()
        {
            //Pick Status screen from the screen selection menu
            app.Tap("Status");

            app.WaitForElement("currentTarget");

            int userInput = 2;

            app.Tap("currentTarget");
            app.ClearText("currentTarget");
            app.EnterText("currentTarget", userInput.ToString());
            app.PressEnter();

            string targetTemperature = app.Query("currentTarget")[0].Text;

            Assert.AreEqual(userInput.ToString(), targetTemperature);
        }

        // Test that the target temperature value in the entry field is incremented by 1
        [Test]
        public void TargetTempIsIncrementedByButton()
        {
            //Pick Status screen from the screen selection menu
            app.Tap("Status");

            app.WaitForElement("currentTarget");

            int startTemp = int.Parse(app.Query("currentTarget")[0].Text);

            app.TapCoordinates(860, 1650);

            string targetTemperature = app.Query("currentTarget")[0].Text;

            Assert.AreEqual((startTemp + 1).ToString(), targetTemperature);
        }

        // Test that the target temperature value in the entry field is decremented by 1
        [Test]
        public void TargetTempIsDecrementedByButton()
        {
            //Pick Status screen from the screen selection menu
            app.Tap("Status");

            app.WaitForElement("currentTarget");

            int startTemp = int.Parse(app.Query("currentTarget")[0].Text);

            app.TapCoordinates(560, 1560);

            string targetTemperature = app.Query("currentTarget")[0].Text;

            Assert.AreEqual((startTemp - 1).ToString(), targetTemperature);
        }
        #endregion

        #region Story 15 UI Tests
        [Test]
        public void TestThatTurningOffNotificationsDisablesNotificationsSubSettings()
        {

        }

        [Test]
        public void TestThatTurningOnNotificationsEnablesNotificationsSubSettings()
        {

        }
        #endregion
    }
}
