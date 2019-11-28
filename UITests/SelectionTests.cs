using NUnit.Framework;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class SelectionTests
    {
        IApp app;
        Platform platform;
        string apkFile = "D:\\virpc\\prj3beer\\prj3.beer\\prj3beer.Android\\bin\\Debug\\com.companyname.prj3beer.apk";

        public SelectionTests(Platform platform)
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
        public void TestSettingsMenuIsDisplayedOnSelectionScreenWhenSettingsButtonIsPressed()
        {
            //Pick Status screen from the screen selection menu
            app.Tap("Selection");

            //Press Settings Menu button
            app.Tap("SettingsMenuButton");

            //Look for the toggle button on the Settings Menu
            AppResult[] temperatureSwitch = app.Query(c => c.Id("temperatureToggle"));

            //Will be greater than 0 if it exists, returns AppResult[]
            Assert.IsTrue(temperatureSwitch.Any());
        }
    }
}
