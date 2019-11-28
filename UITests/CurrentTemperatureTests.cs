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
    class CurrentTemperatureTests
    {

        IApp app;
        Platform platform;
        string apkFile = "D:\\virpc\\prj3beer\\prj3.beer\\prj3beer.Android\\bin\\Debug\\com.companyname.prj3beer.apk";

        public CurrentTemperatureTests(Platform platform)
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
        public void TestTemperatureBelowRangeError()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("CurrentTempBox"));
            Assert.Equals(results[0].Text, "Temperature reading outside of range");
        }

        [Test]
        public void TestTemperatureAboveRangeError()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("currentTemp"));
            Assert.Equals(results[0].Text, "Temperature reading outside of range");
        }

        [Test]
        public void TestDeviceNotFoundError()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("currentTemp"));
            Assert.Equals(results[0].Text, "Waiting for device");
        }

        [Test]
        public void TestTemperatureInRangeAtMinusOne()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("currentTemp"));
            Assert.Equals(results[0].Text, "-1\u00B0C");
        }
    }
}
