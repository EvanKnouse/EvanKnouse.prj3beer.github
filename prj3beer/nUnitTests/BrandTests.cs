using NUnit.Framework;
using prj3beer.Models;
using prj3beer.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Tests
{
    public class Tests 
    {
        Brand Emptybrand = new Brand() { brandID = 3, brandName = "" };
        Brand GWBbrand = new Brand() { brandID = 4, brandName = "Great Western Brewery" };
        Brand CBCbrand = new Brand() { brandID = 5, brandName = "Churchhill Brewing Company" };
        Brand PSBbrand = new Brand() { brandID = 6, brandName = "Prarie Sun Brewery" };
        Brand TooLongbrand = new Brand() { brandID = 7, brandName = "aaaaaaaaaabbbbbbbbbbccccccccccddddddddddeeeeeeeeeeffffffffffg" };

        List<Brand> apibrands = new List<Brand>();
        List<Brand> localBrandList = new List<Brand>();
        LocalStorage db = new LocalStorage();

        APIMockManager api = new APIMockManager();

        [SetUp]
        public async Task SetupAsync()
        {

            localBrandList = db.brandList;
           apibrands = await api.GetBrands();
        }

        [Test]
        public void TestThatValidBrandIsCreatedFromAPI()
        {
            int index = 0;

            for (int i = 0; i < apibrands.Count; i++)
            {
                if(apibrands[i].brandName == "Great Western Brewery")
                {
                    index = i;
                }
            }

            Brand compBrand = apibrands[index] ;

            Assert.IsTrue(compBrand.brandID.CompareTo(GWBbrand.brandID)==0);
            Assert.IsTrue(compBrand.brandName.CompareTo(GWBbrand.brandName) == 0);

        }

        [Test]
        public void TestThatInvalidBrandIsNotCreatedFromAPI()
        {
            Assert.IsFalse(apibrands.Contains(new Brand() {brandID=4, brandName="Great Western Brewery"}));


        }
        [Test]
        public void TestThatSpecificBrandIsAddedToList()
        {
            int index = 0;

            for (int i = 0; i < localBrandList.Count; i++)
            {
                if (localBrandList[i].brandName == "Great Western Brewery")
                {
                    index = i;
                }
            }

            Brand compBrand = localBrandList[index];

            Assert.IsTrue(compBrand.brandID.CompareTo(GWBbrand.brandID) == 0);
            Assert.IsTrue(compBrand.brandName.CompareTo(GWBbrand.brandName) == 0);
        }
        [Test]
        public void TestThatBrandIsAddedToList()
        {
            Assert.IsTrue(localBrandList.Contains(GWBbrand));
        }
        [Test]
        public void TestThatBrandWithTooManyCharactersIsNotAddedToBrandList()
        {
            Assert.IsTrue(!localBrandList.Contains(TooLongbrand));
        }

        [Test]
        public void TestThatValidBrandsAreStoredLocally()
        {
            Assert.IsTrue(localBrandList.Contains(GWBbrand));
            Assert.IsTrue(localBrandList.Contains(PSBbrand));
            Assert.IsTrue(localBrandList.Contains(CBCbrand));

        }

        [Test]
        public void TestThatInvalidBrandsAreNotStoredLocally()
        {
            Assert.IsTrue(!localBrandList.Contains(Emptybrand));
            Assert.IsTrue(!localBrandList.Contains(TooLongbrand));

        }

        [Test]
        public void TestThatListIsPopulatedFromLocalStorage()
        {
            Assert.IsTrue(localBrandList.Count == 3);
        }

        [Test]
        public void TestThatListIsNotPopulatedFromLocalStorageWithIncorrectValues()
        {
            Assert.IsTrue(!localBrandList.Contains(Emptybrand));
            Assert.IsTrue(!localBrandList.Contains(TooLongbrand));
            Assert.IsTrue(localBrandList.Count == 3);
        }




    }
}