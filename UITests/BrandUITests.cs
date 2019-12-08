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
        string apkPath = "D:\\COSACPMG\\prj3.beer\\prj3beer\\prj3beer\\prj3beer.Android\\bin\\Debug\\com.companyname.prj3beer.apk";

        IApp app;
        Platform platform;

        #region Brand Initializers
        Brand EmptyIDBrand = new Brand() { brandName = "Molson Coors Brewing Company" };
        Brand NegativeIDBrand = new Brand() { brandID = -1, brandName = "Molson Coors Brewing Company" };
        Brand Emptybrand = new Brand() { brandID = 3, brandName = "" };
        Brand GWBbrand = new Brand() { brandID = 4, brandName = "Great Western Brewery" };
        Brand CBCbrand = new Brand() { brandID = 5, brandName = "Churchhill Brewing Company" };
        Brand PSBbrand = new Brand() { brandID = 6, brandName = "Prarie Sun Brewery" };
        Brand TooLongbrand = new Brand() { brandID = 7, brandName = new string('a', 61) };
        Brand MaxBoundaryNamebrand = new Brand() { brandID = 7, brandName = new string('a', 60) };
        Brand MinBoundaryNamebrand = new Brand() { brandID = 7, brandName = new string('a', 1) };
        #endregion

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
            app.Tap("Selection");
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

        [Test]
        public void TestThatValidBrandIsAddedToList()
        {
            //Assert.IsTrue(localBrandList.Contains(GWBbrand));
        }

        [Test]
        public void TestThatValidBrandsAreStoredLocally()
        {
            //Assert.IsTrue(localBrandList.Contains(GWBbrand));
            //Assert.IsTrue(localBrandList.Contains(PSBbrand));
            //Assert.IsTrue(localBrandList.Contains(CBCbrand));
        }

        [Test]
        public void TestThatInvalidBrandIsNotAddedToBrandList()
        {
            //Assert.IsTrue(!localBrandList.Contains(Emptybrand));
        }

        [Test]
        public void TestThatSpecificBrandIsAddedToList()
        {
            //int index = 0;

            //for (int i = 0; i < localBrandList.Count; i++)
            //{
            //    if (localBrandList[i].brandName == "Great Western Brewery")
            //    {
            //        index = i;
            //    }
            //}

            //Brand compBrand = localBrandList[index];

            //Assert.IsTrue(compBrand.brandID.CompareTo(GWBbrand.brandID) == 0);
            //Assert.IsTrue(compBrand.brandName.CompareTo(GWBbrand.brandName) == 0);
        }

        [Test]
        public void TestThatInvalidBrandsAreNotStoredLocally()
        {
            //Assert.IsTrue(!localBrandList.Contains(Emptybrand));
            //Assert.IsTrue(!localBrandList.Contains(TooLongbrand));

        }

        [Test]
        public void TestThatListIsPopulatedFromLocalStorage()
        {
            //Assert.IsTrue(localBrandList.Count == 3);
        }

        [Test]
        public void TestThatListIsNotPopulatedFromLocalStorageWithIncorrectValues()
        {
            //Assert.IsTrue(!localBrandList.Contains(Emptybrand));
            //Assert.IsTrue(!localBrandList.Contains(TooLongbrand));
            //Assert.IsTrue(localBrandList.Count == 3);
        }

        [Test]
        public void TestThatASuccessfulConnectionToTheAPIIsEstablished()
        {
            //Assert.IsNotNull(api);
        }

        [Test]
        public void TestThatUnsuccessfulConnectionToTheAPIIsNotMade()
        {
            //api = null;

            //Assert.IsNull(api);
        }
    }
}
