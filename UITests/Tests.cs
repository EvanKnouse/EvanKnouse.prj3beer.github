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
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;
        MockBeverage mockBev;

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
            MockScreen.displayTargetTemp(mockBev);

            Assert.IsTrue(MockScreen.lblTargetTemp.Equals("0"));


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
