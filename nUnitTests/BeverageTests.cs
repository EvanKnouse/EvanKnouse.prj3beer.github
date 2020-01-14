using NUnit.Framework;
using prj3beer.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace nUnitTests
{
    class BeverageTests 
    {
        #region Initializers
        // Objects
        static Brand GWBbrand = new Brand() { BrandID = 1, Name = "Great Western Brewing Company" };
        Beverage GreatWestRadler = new Beverage { BeverageID = 1, Brand = GWBbrand, Name = "Great Western Radler", Temperature = 3, Type = Type.Radler };

        //Create a container for validation results (error messages)
        IList<ValidationResult> errors;
        #endregion

        // Setup the Beverage object for testing
        [SetUp]
        public void Setup()
        {
            GreatWestRadler = new Beverage { BeverageID = 1, Brand = GWBbrand, Name = "Great Western Radler", Temperature = 3, Type = Type.Radler };
        }

        [Test]
        public void TestThatMinBoundaryBeverageIDIsValid()
        {
            GreatWestRadler.BeverageID = 1;
            errors = ValidationHelper.Validate(GreatWestRadler);
            Assert.IsTrue(errors.Count == 0);
        }

        [Test]
        public void TestThatBelowMinBoundaryBeverageIDIsInvalid()
        {
            GreatWestRadler.BeverageID = 0;
            errors = ValidationHelper.Validate(GreatWestRadler);
            Assert.IsTrue(errors.Count == 1);
            Assert.AreEqual(errors[0].ToString(), "ID Range must be between 1 and 999");
        }

        [Test]
        public void TestThatMaxBoundaryBeverageNameIsValid()
        {
            GreatWestRadler.Name = new string('a', 40);
            errors = ValidationHelper.Validate(GreatWestRadler);
            Assert.IsTrue(errors.Count == 0);
        }

        [Test]
        public void TestThatBeverageHasBrand()
        {
            errors = ValidationHelper.Validate(GreatWestRadler);
            Assert.IsTrue(errors.Count == 0);
        }

        [Test]
        public void TestThatMinBoundaryBeverageNameIsValid()
        {
            GreatWestRadler.Name = new string('a', 3);
            errors = ValidationHelper.Validate(GreatWestRadler);
            Assert.IsTrue(errors.Count == 0);
        }

        [Test]
        public void TestThatBeverageHasMissingBrand()
        {
            GreatWestRadler.Brand = null;
            errors = ValidationHelper.Validate(GreatWestRadler);
            Assert.IsTrue(errors.Count == 1);
        }

        [Test]
        public void TestThatBelowMinBeverageNameIsInvalid()
        {
            GreatWestRadler.Name = new string('a', 2);
            errors = ValidationHelper.Validate(GreatWestRadler);
            Assert.IsTrue(errors.Count == 1);
            Assert.AreEqual(errors[0].ToString(), "Beverage Name Too Short, 3 Characters Minimum");
        }

        [Test]
        public void TestThatAboveMaxBeverageNameIsInvalid()
        {
            GreatWestRadler.Name = new string('a', 41);
            errors = ValidationHelper.Validate(GreatWestRadler);
            Assert.IsTrue(errors.Count == 1);
            Assert.AreEqual(errors[0].ToString(), "Beverage Name Too Long, 40 Characters Maximum");
        }

        [Test]
        public void TestThatBelowMinBeverageIDIsInvalid()
        {
            GreatWestRadler.BeverageID = -1;
            errors = ValidationHelper.Validate(GreatWestRadler);
            Assert.IsTrue(errors.Count == 1);
            Assert.AreEqual(errors[0].ToString(), "ID Range must be between 1 and 999");
        }

        [Test]
        public void TestThatBeverageTypeIsValid()
        {
            GreatWestRadler.Type = Type.Radler;
            errors = ValidationHelper.Validate(GreatWestRadler);
            Assert.IsTrue(errors.Count == 0);
        }

        [Test]
        public void TestThatBeverageTypeIsInvalid()
        {

            GreatWestRadler.Type = null;
            errors = ValidationHelper.Validate(GreatWestRadler);
            Assert.IsTrue(errors.Count == 1);
            Assert.AreEqual(errors[0].ToString(), "Type is Required");

        }

        [Test]
        public void TestThatMinTemperatureIsValid()
        {
            GreatWestRadler.Temperature = -30;
            errors = ValidationHelper.Validate(GreatWestRadler);
            Assert.IsTrue(errors.Count == 0);
        }

        [Test]
        public void TestThatMaxTemperatureIsValid()
        {
            GreatWestRadler.Temperature = 30;
            errors = ValidationHelper.Validate(GreatWestRadler);
            Assert.IsTrue(errors.Count == 0);
        }

        [Test]
        public void TestThatBelowMinTemperatureIsInvalid()
        {
            GreatWestRadler.Temperature = -31;
            errors = ValidationHelper.Validate(GreatWestRadler);
            Assert.IsTrue(errors.Count == 1);
            Assert.AreEqual(errors[0].ToString(), "Target Temperature cannot be below -30C or above 30C");
        }

        [Test]
        public void TestThatAboveMaxTemperatureIsInvalid()
        {
            GreatWestRadler.Temperature = 31;
            errors = ValidationHelper.Validate(GreatWestRadler);
            Assert.IsTrue(errors.Count == 1);
            Assert.AreEqual(errors[0].ToString(), "Target Temperature cannot be below -30C or above 30C");
        }
    }
}