using NUnit.Framework;
using prj3beer.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace nUnitTests
{
    class BrandTests 
    {
        #region Initializers
        Brand EmptyIDBrand = new Brand() { Name = "Molson Coors Brewing Company" };
        Brand NegativeIDBrand = new Brand() { BrandID = -1, Name = "Molson Coors Brewing Company" };
        Brand Emptybrand = new Brand() { BrandID = 3, Name = "" };
        Brand GWBbrand = new Brand() { BrandID = 4, Name = "Great Western Brewery" };
        Brand CBCbrand = new Brand() { BrandID = 5, Name = "Churchhill Brewing Company" };
        Brand PSBbrand = new Brand() { BrandID = 6, Name = "Prarie Sun Brewery" };
        Brand TooLongbrand = new Brand() { BrandID = 7, Name = new string('a', 61) };
        Brand MaxBoundaryNamebrand = new Brand() { BrandID = 7, Name = new string('a', 40) };
        Brand MinBoundaryNamebrand = new Brand() { BrandID = 7, Name = new string('a', 3) };

        //Create a container for validation results (error messages)
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
            Assert.IsTrue(errors[0].ErrorMessage == "Brand Name Too Long, 40 Characters Maximum");
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