using NUnit.Framework;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using prj3beer.Models;
using Xamarin.Forms;

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
            //Pick Status screen from the screen selection menu
            app.Tap("Status");

            //Wait for the Settings button to appear on screen
            app.WaitForElement("Settings");

            //Press Settings menu button
            app.Tap("Settings");

            //Wait for the Temperature switch to appear on screen
            app.WaitForElement("switchTemp");

            //Tap on the toggle button to change the temperature setting to fahrenheit
            app.Tap("switchTemp");

            //Go back to the Status screen
            app.Back();

            //Wait for the Current Temperature Label to appear on screen
            app.WaitForElement("currentTemperature");

            //Check that the label for the current temperature is set to "\u00B0F"
            string tempLabel = app.Query("currentTemperature")[0].Text;

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

            //Tap on the toggle button to change the temperature setting to fahrenheit
            app.Tap("switchTemp");

            //Grab the temperature label text to prove it switched
            string toggled = app.Query("lblTemp")[0].Text;

            //Check if the enabled value is true
            Assert.AreEqual(toggled, "Fahrenheit");
        }
    }
}
