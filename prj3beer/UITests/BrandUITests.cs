using System;
using System.IO;
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
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void WelcomeTextIsDisplayed()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("Welcome to Xamarin.Forms!"));
            app.Screenshot("Welcome screen.");

            Assert.IsTrue(results.Any());
        }

        [Test]
        public void TestThatListViewExistsOnPage()
        {
            AppResult[] brandList = app.Query("brandList");
            Assert.IsTrue(brandList.Any());
        
        }

        [Test]
        public void TestThatListContainsValidBrands()
        {
            AppResult[] brandList = app.Query("brandList");
            foreach (AppResult brand in brandList)
            {
                   //brand.Text
            }
        }

        [Test]
        public void TestThatListDoesNotContainInvalidBrands()
        {

        }

    }
}
