using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using prj3beer.Models;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class BrandUITests
    {
        string apkFile = "D:\\virpc\\prj3beer\\prj3.beer\\prj3beer\\prj3beer.Android\\bin\\Debug\\com.companyname.prj3beer.apk";
                        //D:\virpc\prj3beer\prj3.beer\prj3beer\prj3beer.Android\bin\Debug
    IApp app;
        Platform platform;

        #region Brand Initializers
        Brand EmptyIDBrand = new Brand() { Name = "Molson Coors Brewing Company" };
        Brand NegativeIDBrand = new Brand() { BrandID = -1, Name = "Molson Coors Brewing Company" };
        Brand Emptybrand = new Brand() { BrandID = 3, Name = "" };
        Brand GWBbrand = new Brand() { BrandID = 4, Name = "Great Western Brewery" };
        Brand CBCbrand = new Brand() { BrandID = 5, Name = "Churchhill Brewing Company" };
        Brand PSBbrand = new Brand() { BrandID = 6, Name = "Prarie Sun Brewery" };
        Brand TooLongbrand = new Brand() { BrandID = 7, Name = new string('a', 61) };
        Brand MaxBoundaryNamebrand = new Brand() { BrandID = 7, Name = new string('a', 60) };
        Brand MinBoundaryNamebrand = new Brand() { BrandID = 7, Name = new string('a', 1) };
        #endregion

        public BrandUITests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            //app = AppInitializer.StartApp(platform);
            app = ConfigureApp.Android.ApkFile(apkFile).StartApp();
            app.TapCoordinates(150, 90);
            app.Tap("Brand Select");
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
            AppResult[] brandList = app.Query(GWBbrand.Name);
            Assert.IsTrue(brandList.Any());
            brandList = app.Query(CBCbrand.Name);
            Assert.IsTrue(brandList.Any());
            brandList = app.Query(PSBbrand.Name);
            Assert.IsTrue(brandList.Any());
        }

        [Test]
        public void TestThatListDoesNotContainInvalidBrands()
        {
            app.WaitForElement("brandList");
            AppResult[] brandList = app.Query(Emptybrand.Name);
            Assert.IsFalse(brandList.Any());
            brandList = app.Query(TooLongbrand.Name);
            Assert.IsFalse(brandList.Any());
        }

        [Test]
        public void TestThatSpecificBrandIsAddedToList()
        {
            app.WaitForElement("brandList");
            AppResult[] brandList = app.Query(GWBbrand.Name);
            Assert.AreEqual("Great Western Brewery", brandList.ElementAt(0).Text);
        }

        //Test borrowed from https://stackoverflow.com/questions/44113109/
        // how-to-validate-alphabetical-order-of-tableview-elements-in-xamarin-ui-tests
        [Test]
        public void TestThatListIsSortedAlphabetically()
        {
            app.WaitForElement("brandList");
            AppResult[] brandList = app.Query("brandList");
            //List<string> listOfBrands = new List<string>();

            var ascending = brandList.OrderBy(a => a.Text);

            Assert.IsTrue(brandList.SequenceEqual(ascending));

        }
    }
}
