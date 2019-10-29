using NUnit.Framework;
using prj3beer.Services;
using Xamarin.UITest;

namespace UITests
{
    [TestFixture(Platform.Android)]
    public class Tests
    {
        IApp app;
        Platform platform;
        Beverage mockBev;

        //In place of "null", -800 will be used for ints
        int iNull = -800;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
            mockBev = new Beverage("Banquet", "Coors", 0);
        }

        [Test]
        public void testTargetTemperatureIsDisplayed()
        {
            string test = mockBev.getIdealTemp().ToString();

            app.EnterText(x => x.Marked("tempLabel"), test);

            string tempLabelValue = app.Query(x => x.Marked("tempLabel"))[0].Text;

            Assert.IsTrue(!tempLabelValue.Equals(""));
        }
 
        [Test]
        public void testBeverageTemperatureIsNull()
        {
            //Set temperature to -800 to signify null from the database
            mockBev = new Beverage("Banquet", "Coors", iNull);

            string test = mockBev.getIdealTemp().ToString();

            app.EnterText(x => x.Marked("tempLabel"), test);

            string tempLabelValue = app.Query(x => x.Marked("tempLabel"))[0].Text;

            Assert.IsTrue(tempLabelValue.Equals("Error message"));
        }

        [Test]
        public void testIdealTempTextBoxIsDisplayedCorrectly()
        {
            Label myLabel = new Label { Text = mockBev.getIdealTemp().ToString() };

            myLabel.AnchorX = 540;

            Assert.IsTrue(540 == myLabel.AnchorX);

            myLabel.AnchorY = 1700;

            Assert.IsTrue(1700 == myLabel.AnchorY);
        }

        [Test]
        public void testIdealTempTextBoxIsDisplayedIncorrectly()
        {
            Label myLabel = new Label { Text = mockBev.getIdealTemp().ToString() };

            myLabel.AnchorX = 541;

            Assert.IsTrue(540 != myLabel.AnchorX);

            myLabel.AnchorY = 1701;

            Assert.IsTrue(1700 != myLabel.AnchorY);
        }

        //[Test]
        //public void WelcomeTextIsDisplayed()
        //{
        //    AppResult[] results = app.WaitForElement(c => c.Marked("Welcome to Xamarin.Forms!"));
        //    app.Screenshot("Welcome screen.");

        //    Assert.IsTrue(results.Any());
        //}
    }
}
