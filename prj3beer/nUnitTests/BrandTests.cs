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
        List<Brand> brands = new List<Brand>();


        APIMockManager api = new APIMockManager();

        [SetUp]
        public async Task SetupAsync()
        {
           brands = await api.GetBrands();
           apibrands = await api.GetBrands();
        }

        [Test]
        public void TestThatValidBrandIsCreatedFromAPI()
        {
            int index = 0;

            for (int i = 0; i < brands.Count; i++)
            {
                if(brands[i].brandName == "Great Western Brewery")
                {
                    index = i;
                }
            }

            Brand compBrand = brands[index] ;

            Assert.IsTrue(compBrand.brandID.CompareTo(GWBbrand.brandID)==0);
            Assert.IsTrue(compBrand.brandName.CompareTo(GWBbrand.brandName) == 0);

        }

        [Test]
        public void TestThatInvalidBrandIsNotCreatedFromAPI()
        {
            Assert.IsFalse(brands.Contains(new Brand() {brandID=4, brandName="Great Western Brewery"}));


        }
        [Test]
        public void TestThatSpecificBrandIsAddedToList()
        {
            int index = 0;

            for (int i = 0; i < brands.Count; i++)
            {
                if (brands[i].brandName == "Great Western Brewery")
                {
                    index = i;
                }
            }

            Brand compBrand = brands[index];

            Assert.IsTrue(compBrand.brandID.CompareTo(GWBbrand.brandID) == 0);
            Assert.IsTrue(compBrand.brandName.CompareTo(GWBbrand.brandName) == 0);
        }
        [Test]
        public void TestThatBrandIsAddedToList()
        {


        }
        [Test]
        public void TestThatBrandWithTooManyCharactersIsNotAddedToBrandList()
        {
            Assert.IsFalse(brands.Contains(TooLongbrand));
        }

    }
}