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
                AppResult[] results = app.Query("Status");
                Assert.IsTrue(results.Any());
            }
        }

    }
}
