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
        Brand noIDBrand = new Brand() { brandID = -1, brandName = new string('a', 1) };

        #endregion


        [SetUp]
        public async Task SetupAsync()
        {
           BeerContext bc = new BeerContext();
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
        public void TestThatBrandIDIsNotEmpty()
        {
            Assert.IsTrue(ValidationHelper.Validate(GWBbrand).Count == 0);
        }

        [Test]
        public void TestThatEmptyBrandIDIsInvalid()
        {
            Assert.IsTrue(ValidationHelper.Validate(noIDBrand).Count == 1);
        }


    }
}