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
    class statusPageUITests
    {

        [TestFixture(Platform.Android)]
        public class Tests
        {
            IApp app;
            Platform platform;

            public Tests(Platform platform)
            {
                this.platform = platform;
            }

            [SetUp]
            public void BeforeEachTest()
            {
                //app = AppInitializer.StartApp(platform);
                app = ConfigureApp.Android.ApkFile(@"D:\prj3beer\prj3.beer\prj3beer\prj3beer.Android\bin\Debug\com.companyname.prj3beer.apk").StartApp();
                app.TapCoordinates(150, 90);
                //app.Tap("ScreenSelectButton");
                app.Tap("Status");
            }

            [Test]
            public void AppIsOnStatusPage()
            {
                app.WaitForElement("StatusPage");

                AppResult[] results = app.Query("StatusPage");

                Assert.IsTrue(results.Any());
            }

            [Test]
            public void TargetTempEntryIsOnPage()
            {
                app.WaitForElement("currentTarget");

                AppResult[] results = app.Query("currentTarget");

                Assert.IsTrue(results.Any());
            }

            [Test]
            public void IncrementButtonIsOnPage()
            {
                app.WaitForElement("btnIncTemp");

                AppResult[] results = app.Query("btnIncTemp");
                Assert.IsTrue(results.Any());
            }

            [Test]
            public void DecrementButtonIsOnPage()
            {
                app.WaitForElement("btnDecTemp");

                AppResult[] results = app.Query("btnDecTemp");
                Assert.IsTrue(results.Any());
            }

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

            [Test]
            public void TargetTempIsIncrementedByButton()
            {
                app.WaitForElement("currentTarget");

                int startTemp = int.Parse(app.Query("currentTarget")[0].Text);

                app.Tap("btnIncTemp");

                String targetTemperature = app.Query("currentTarget")[0].Text;

                Assert.AreEqual((startTemp + 1).ToString(), targetTemperature);
            }

            [Test]
            public void TargetTempIsDecrementedByButton()
            {
                app.WaitForElement("currentTarget");

                int startTemp =  int.Parse(app.Query("currentTarget")[0].Text);

                app.Tap("btnDecTemp");

                String targetTemperature = app.Query("currentTarget")[0].Text;

                Assert.AreEqual((startTemp - 1).ToString(), targetTemperature);
            }


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
