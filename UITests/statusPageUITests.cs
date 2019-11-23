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
                app.Tap("HomeMenu");
                app.Tap("Status");
            }


            [Test]
            public void AppIsOnStatusPage()
            {
                AppResult[] results = app.Query("StatusPage");
                Assert.IsTrue(results.Any());
            }

            [Test]
            public void TargetTempEntryIsOnPage()
            {
                AppResult[] results = app.Query("currentTarget");
                Assert.IsTrue(results.Any());
            }

            [Test]
            public void IncrementButtonIsOnPage()
            {
                AppResult[] results = app.Query("btnIncTemp");
                Assert.IsTrue(results.Any());
            }

            [Test]
            public void DecrementButtonIsOnPage()
            {
                AppResult[] results = app.Query("btnDecTemp");
                Assert.IsTrue(results.Any());
            }

            [Test]
            public void TargetTempEntryFieldCanBeSetManually()
            {
                app.EnterText(c => c.Marked("entryEmail"), "jimBob@hotmail.com");

                int userInput = 5;

                app.Tap("currentTarget");
                app.EnterText("currentTarget", userInput.ToString());

                String targetTemperature = app.Query("currentTarget")[0].Text;

                Assert.AreEqual(userInput.ToString(), targetTemperature);
            }

            [Test]
            public void TargetTempIsIncrementedByButton()
            {
                int startTemp = int.Parse(app.Query("currentTarget")[0].Text);

                app.Tap("btnIncTemp");

                String targetTemperature = app.Query("currentTarget")[0].Text;

                Assert.AreEqual((startTemp + 1).ToString(), targetTemperature);
            }

            [Test]
            public void TargetTempIsDecrementedByButton()
            {
                int startTemp =  int.Parse(app.Query("currentTarget")[0].Text);

                app.Tap("btnDecTemp");

                String targetTemperature = app.Query("currentTarget")[0].Text;

                Assert.AreEqual((startTemp - 1).ToString(), targetTemperature);
            }


            [Test]
            public void TargetTempIsChangedInFahrenheit()
            {
                int startTemp = int.Parse(app.Query("currentTarget")[0].Text);

                app.Tap("btnIncTemp");

                //Still need to implement
            }





        }

    }
}
