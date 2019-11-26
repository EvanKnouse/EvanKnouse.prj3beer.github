using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITests
{
    class StatusPageUITests
    {
        [TestFixture(Platform.Android)]
        public class Tests
        {
            // Objects
            IApp app;
            Platform platform;

            // Constructor for the Tests class, initialize the platform object
            public Tests(Platform platform)
            {
                this.platform = platform;
            }

            // Start the application and navigate to the status screen before each individual test is ran
            [SetUp]
            public void BeforeEachTest()
            {
                //app = AppInitializer.StartApp(platform);
                app = ConfigureApp.Android.ApkFile(@"D:\prj3beer\prj3.beer\prj3beer\prj3beer.Android\bin\Debug\com.companyname.prj3beer.apk").StartApp();
                app.TapCoordinates(150, 90);
                //app.Tap("ScreenSelectButton");
                app.Tap("Status");
            }

            // Test that the application is currently on the status page
            [Test]
            public void AppIsOnStatusPage()
            {
                app.WaitForElement("StatusPage");

                AppResult[] results = app.Query("StatusPage");

                Assert.IsTrue(results.Any());
            }

            // Test that the target temperature entry field is on the status page
            [Test]
            public void TargetTempEntryIsOnPage()
            {
                app.WaitForElement("currentTarget");

                AppResult[] results = app.Query("currentTarget");

                Assert.IsTrue(results.Any());
            }

            // Test that the target temperature increment button is on the status page
            [Test]
            public void IncrementButtonIsOnPage()
            {
                app.WaitForElement("btnIncTemp");

                AppResult[] results = app.Query("btnIncTemp");
                Assert.IsTrue(results.Any());
            }

            // Test that the target temperature decrement button is on the status page
            [Test]
            public void DecrementButtonIsOnPage()
            {
                app.WaitForElement("btnDecTemp");

                AppResult[] results = app.Query("btnDecTemp");
                Assert.IsTrue(results.Any());
            }

            // Test that a temperature value can be entered in the entry field on the status page
            // The entry field does not currently clear when being tapped, so this test fails with "Test5Target" instead of "5"
            [Test]
            public void TargetTempEntryFieldCanBeSetManually()
            {
                //app.EnterText(c => c.Marked("entryEmail"), "jimBob@hotmail.com");
                app.WaitForElement("currentTarget");

                int userInput = 5;

                app.Tap("currentTarget");
                app.EnterText("currentTarget", userInput.ToString());

                String targetTemperature = app.Query("currentTarget")[0].Text;

                Assert.AreEqual(userInput.ToString(), targetTemperature);
            }

            // Test that the target temperature value in the entry field is incremented by 1
            [Test]
            public void TargetTempIsIncrementedByButton()
            {
                app.WaitForElement("currentTarget");

                int startTemp = int.Parse(app.Query("currentTarget")[0].Text);

                app.Tap("btnIncTemp");

                String targetTemperature = app.Query("currentTarget")[0].Text;

                Assert.AreEqual((startTemp + 1).ToString(), targetTemperature);
            }

            // Test that the target temperature value in the entry field is decremented by 1
            [Test]
            public void TargetTempIsDecrementedByButton()
            {
                app.WaitForElement("currentTarget");

                int startTemp =  int.Parse(app.Query("currentTarget")[0].Text);

                app.Tap("btnDecTemp");

                String targetTemperature = app.Query("currentTarget")[0].Text;

                Assert.AreEqual((startTemp - 1).ToString(), targetTemperature);
            }

            // Not yet implemented
            [Test]
            public void TargetTempIsChangedInFahrenheit()
            {
                app.WaitForElement("currentTarget");

                int startTemp = int.Parse(app.Query("currentTarget")[0].Text);

                app.Tap("btnIncTemp");

                //Still need to implement
            }
        }
    }
}