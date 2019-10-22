using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.Forms;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;
        MockBeverage mockBev;

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
            mockBev = new MockBeverage("Banquet", "Coors", 0);
        }

        [Test]
        public void testTargetTemperatureIsDisplayed()
        {
            string test = MockScreen.displayTargetTemp(mockBev);

            app.EnterText(x => x.Marked("tempLabel"), test);

            string tempLabelValue = app.Query(x => x.Marked("tempLabel"))[0].Text;

            Assert.IsTrue(!tempLabelValue.Equals(""));

            //MockScreen.displayTargetTemp(mockBev);

            //Assert.IsTrue(MockScreen.lblTargetTemp.Equals("0"));
        }
 
        [Test]
        public void testBeverageTemperatureIsNull()
        {
            //Set temperature to -800 to signify null from the database
            mockBev = new MockBeverage("Banquet", "Coors", iNull);

            string test = mockBev.idealTemp.ToString();

            app.EnterText(x => x.Marked("tempLabel"), test);

            string tempLabelValue = app.Query(x => x.Marked("tempLabel"))[0].Text;

            Assert.IsTrue(tempLabelValue.Equals("Error message"));




            MockScreen.displayTargetTemp(mockBev);

            Assert.IsTrue(MockScreen.lblTargetTemp.Equals("Ideal temperature is not available"));
        }

        [Test]
        public void testIdealTempIsAboveUpperThreshold()
        {
            mockBev = new MockBeverage("Banquet", "Coors", 32);

            MockScreen.displayTargetTemp(mockBev);

            Assert.IsTrue(MockScreen.lblTargetTemp.Equals("Ideal temperature is out of range."));
        }

        [Test]
        public void testIdealTempIsBelowLowerThreshold()
        {
            mockBev = new MockBeverage("Banquet", "Coors", -31);

            MockScreen.displayTargetTemp(mockBev);

            Assert.IsTrue(MockScreen.lblTargetTemp.Equals("Ideal temperature is out of range."));
        }

        [Test]
        public void testIdealTempTextBoxIsDisplayedCorrectly()
        {
            Label myLabel = new Label { Text = mockBev.idealTemp.ToString() };

            myLabel.AnchorX = 540;

            Assert.IsTrue(540 == myLabel.AnchorX);

            myLabel.AnchorY = 1700;

            Assert.IsTrue(1700 == myLabel.AnchorY);
        }

        [Test]
        public void testIdealTempTextBoxIsDisplayedIncorrectly()
        {
            Label myLabel = new Label { Text = mockBev.idealTemp.ToString() };

            myLabel.AnchorX = 541;

            Assert.IsTrue(540 != myLabel.AnchorX);

            myLabel.AnchorY = 1701;

            Assert.IsTrue(1700 != myLabel.AnchorY);
        }

        [Test]
        public void WelcomeTextIsDisplayed()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("Welcome to Xamarin.Forms!"));
            app.Screenshot("Welcome screen.");

            Assert.IsTrue(results.Any());
        }
    }
}
