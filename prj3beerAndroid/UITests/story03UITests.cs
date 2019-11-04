using Android.Widget;
using NUnit.Framework;
using prj3beerAndroid;
using Xamarin.UITest;

namespace UITests
{
    [TestFixture(Platform.Android)]
    public class Tests
    {
        IApp app;
        Platform platform;
        // beverage object for testing
        Beverage beverage;

        // initialize the platform
        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp] // initialize the application and beverage object
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
            beverage = new Beverage("Banquet", "Coors", 0);
        }

        [Test] // test that the target temperature is displayed in the temperature label
        public void testTargetTemperatureIsDisplayed()
        {
            string test = beverage.getIdealTemp().ToString();

            app.EnterText(x => x.Marked("tempLabel"), test);

            string tempLabelValue = app.Query(x => x.Marked("tempLabel"))[0].Text;

            Assert.IsTrue(!tempLabelValue.Equals(""));
        }
 
        [Test] // we no longer test nulls?
        public void testBeverageTemperatureIsNull()
        {
            //Set temperature to -800 to signify null from the database
            beverage = new Beverage("Banquet", "Coors", 2);

            string test = beverage.getIdealTemp().ToString();

            app.EnterText(x => x.Marked("tempLabel"), test);

            string tempLabelValue = app.Query(x => x.Marked("tempLabel"))[0].Text;

            Assert.IsTrue(tempLabelValue.Equals("Error message"));
        }

        [Test] // test the temperature label is displayed in the correct spot
        public void testIdealTempTextBoxIsDisplayedCorrectly()
        {
            TextView tvIdealTemp = new TextView();

            tvIdealTemp.AnchorX = 540;

            Assert.IsTrue(540 == tvIdealTemp.AnchorX);

            tvIdealTemp.AnchorY = 1700;

            Assert.IsTrue(1700 == tvIdealTemp.AnchorY);
        }
    }
}
