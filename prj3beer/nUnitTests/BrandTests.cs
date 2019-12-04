using NUnit.Framework;
using prj3beer.Models;
using System.Threading.Tasks;
using prj3beer.Utilities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tests
{
    public class Tests 
    {
        #region Initializers
        Brand EmptyIDBrand = new Brand() { brandName = "Molson Coors Brewing Company" };
        Brand NegativeIDBrand = new Brand() { brandID = -1, brandName = "Molson Coors Brewing Company" };
        Brand Emptybrand = new Brand() { brandID = 3, brandName = "" };
        Brand GWBbrand = new Brand() { brandID = 4, brandName = "Great Western Brewery" };
        Brand CBCbrand = new Brand() { brandID = 5, brandName = "Churchhill Brewing Company" };
        Brand PSBbrand = new Brand() { brandID = 6, brandName = "Prarie Sun Brewery" };
        Brand TooLongbrand = new Brand() { brandID = 7, brandName = new string('a', 61) };
        Brand MaxBoundaryNamebrand = new Brand() { brandID = 7, brandName = new string('a', 60) };
        Brand MinBoundaryNamebrand = new Brand() { brandID = 7, brandName = new string('a', 1) };

        IList<ValidationResult> errors;
        #endregion

        [Test]
        public void TestThatBrandSentThroughValidatorIsValid()
        {
            errors = ValidationHelper.Validate(GWBbrand);
            Assert.IsTrue(errors.Count == 0);
        }

        [Test]
        public void TestThatMaxBoundaryBrandSentThroughValidatorIsValid()
        {
            errors = ValidationHelper.Validate(MaxBoundaryNamebrand);
            Assert.IsTrue(errors.Count == 0);
        }

        [Test]
        public void TestThatMinBoundaryBrandSentThroughValidatorIsValid()
        {
            errors = ValidationHelper.Validate(MinBoundaryNamebrand);
            Assert.IsTrue(errors.Count == 0);
        }

        [Test]
        public void TestThatTooLongBrandSentThroughValidatorIsInvalid()
        {
            errors = ValidationHelper.Validate(TooLongbrand);
            Assert.IsTrue(errors.Count == 1);
            Assert.IsTrue(errors[0].ErrorMessage == "Brand Name Too Long, 60 Characters Maximum");
        }

        [Test]
        public void TestThatEmptyBrandSentThroughValidatorIsInvalid()
        {
            errors = ValidationHelper.Validate(Emptybrand);
            Assert.IsTrue(errors.Count == 1);
            Assert.IsTrue(errors[0].ErrorMessage == "Brand Name Required");
        }

        [Test]
        public void TestThatBrandIDIsValid()
        {
            errors = ValidationHelper.Validate(GWBbrand);
            Assert.IsTrue(errors.Count == 0);
        }

        [Test]
        public void TestThatEmptyBrandIDIsInvalid()
        {
            errors = ValidationHelper.Validate(EmptyIDBrand);
            Assert.IsTrue(errors.Count == 1);
            Assert.IsTrue(errors[0].ErrorMessage == "Brand ID must be a positive number less than 200");
        }

        [Test]
        public void TestThatNegativeBrandIDIsInvalid()
        {
            errors = ValidationHelper.Validate(NegativeIDBrand);
            Assert.IsTrue(errors.Count == 1);
            Assert.IsTrue(errors[0].ErrorMessage == "Brand ID must be a positive number less than 200");
        }
    }
}