using System.Linq;
using NUnit.Framework;
using prj3beer.Models;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class BrandUITests
    {
        string apkPath = "D:\\COSACPMG\\prj3.beer\\prj3beer\\prj3beer\\prj3beer.Android\\bin\\Debug\\com.companyname.prj3beer.apk";

        IApp app;
        Platform platform;

        Brand Emptybrand = new Brand() { brandID = 3, brandName = "" };
        Brand GWBbrand = new Brand() { brandID = 4, brandName = "Great Western Brewery" };
        Brand CBCbrand = new Brand() { brandID = 5, brandName = "Churchhill Brewing Company" };
        Brand PSBbrand = new Brand() { brandID = 6, brandName = "Prarie Sun Brewery" };
        Brand TooLongbrand = new Brand() { brandID = 7, brandName = "aaaaaaaaaabbbbbbbbbbccccccccccddddddddddeeeeeeeeeeffffffffffg" };

        public BrandUITests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            //app = AppInitializer.StartApp(platform);
            app = ConfigureApp.Android.ApkFile(apkPath).StartApp();
            app.TapCoordinates(150, 90);
            app.Tap("Brand");
        }

        [Test]
        public void TestThatListViewExistsOnPage()
        {
            app.WaitForElement("brandList");
            AppResult[] brandList = app.Query("brandList");
            Assert.IsTrue(brandList.Any());
        }

        [Test]
        public void TestThatListContainsValidBrands()
        {
            app.WaitForElement("brandList");
            AppResult[] brandList = app.Query(GWBbrand.brandName);
            Assert.IsTrue(brandList.Any());
            brandList = app.Query(CBCbrand.brandName);
            Assert.IsTrue(brandList.Any());
            brandList = app.Query(PSBbrand.brandName);
            Assert.IsTrue(brandList.Any());
        }

        [Test]
        public void TestThatListDoesNotContainInvalidBrands()
        {
            app.WaitForElement("brandList");
            AppResult[] brandList = app.Query(Emptybrand.brandName);
            Assert.IsFalse(brandList.Any());
            brandList = app.Query(TooLongbrand.brandName);
            Assert.IsFalse(brandList.Any());
        }

    }
}
