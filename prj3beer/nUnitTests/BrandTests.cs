using NUnit.Framework;
using prj3beer.Models;
using System.Threading.Tasks;
using prj3beer.Utilities;

namespace Tests
{
    public class Tests 
    {
        #region Initializers
        Brand Emptybrand = new Brand() { brandID = 3, brandName = "" };
        Brand GWBbrand = new Brand() { brandID = 4, brandName = "Great Western Brewery" };
        Brand CBCbrand = new Brand() { brandID = 5, brandName = "Churchhill Brewing Company" };
        Brand PSBbrand = new Brand() { brandID = 6, brandName = "Prarie Sun Brewery" };
        Brand TooLongbrand = new Brand() { brandID = 7, brandName = new string('a', 61) };
        Brand MaxBoundaryNamebrand = new Brand() { brandID = 7, brandName = new string('a', 60) };
        Brand MinBoundaryNamebrand = new Brand() { brandID = 7, brandName = new string('a', 1) };


        #endregion


        [SetUp]
        public async Task SetupAsync()
        {
            bc = new BeerContext();
        }


        [Test]
        public void TestThatBrandSentThroughValidatorIsValid()
        {
            Assert.IsTrue(ValidationHelper.Validate(GWBbrand).Count == 0);
        }

        [Test]
        public void TestThatMaxBoundaryBrandSentThroughValidatorIsValid()
        {
            Assert.IsTrue(ValidationHelper.Validate(MaxBoundaryNamebrand).Count == 0);
        }

        [Test]
        public void TestThatMinBoundaryBrandSentThroughValidatorIsValid()
        {
            Assert.IsTrue(ValidationHelper.Validate(MinBoundaryNamebrand).Count == 0);
        }


        [Test]
        public void TestThatTooLongBrandSentThroughValidatorIsInvalid()
        {
            Assert.IsFalse(ValidationHelper.Validate(TooLongbrand).Count==0);
        }

        [Test]
        public void TestThatEmptyBrandSentThroughValidatorIsInvalid()
        {
            Assert.IsFalse(ValidationHelper.Validate(Emptybrand).Count == 0);
        }

        [Test]
        public void TestThatValidBrandIsAddedToList()
        {
            Assert.IsTrue(localBrandList.Contains(GWBbrand));
        }

        [Test]
        public void TestThatValidBrandsAreStoredLocally()
        {
            Assert.IsTrue(localBrandList.Contains(GWBbrand));
            Assert.IsTrue(localBrandList.Contains(PSBbrand));
            Assert.IsTrue(localBrandList.Contains(CBCbrand));
        }

        [Test]
        public void TestThatInvalidBrandIsNotAddedToBrandList()
        {
            Assert.IsTrue(!localBrandList.Contains(Emptybrand));
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

        [Test]
        public void TestThatASuccessfulConnectionToTheAPIIsEstablished()
        {
            Assert.IsNotNull(api);
        }

        [Test]
        public void TestThatUnsuccessfulConnectionToTheAPIIsNotMade()
        {
            api = null;

            Assert.IsNull(api);
        }


    }
}