using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using prj3beer.Models;
using System.ComponentModel.DataAnnotations;

namespace nUnitTests
{
    class BeverageTests
    {
        // Objects
        Beverage mockBev;

        // Setup the Beverage object for testing
        [SetUp]
        public void Setup()
        {
            mockBev = new Beverage();
            mockBev.BeverageID = 1;
            mockBev.Temperature = 2;
        }


        [Test]
        public void TestThatBeverageHasValidBeverageID()
        {
            mockBev.BeverageID = 3;

            var errors = ValidationHelper.Validate(mockBev);

            Assert.IsTrue(errors.Count == 0);
        }

        [Test]
        public void TestThatBeverageHasValidTemperatureSetToUpperBoundary()
        {
            mockBev.Temperature = 30;

            var errors = ValidationHelper.Validate(mockBev);

            Assert.IsTrue(errors.Count == 0);
        }

        [Test]
        public void TestThatBeverageHasValidTemperatureSetToLowerBoundary()
        {
            mockBev.Temperature = -30;

            var errors = ValidationHelper.Validate(mockBev);

            Assert.IsTrue(errors.Count == 0);
        }

        [Test]
        public void TestThatBeverageHasInValidTemperatureSetToUpperBoundaryPlusOne()
        {
            mockBev.Temperature = 31;

            var errors = ValidationHelper.Validate(mockBev);

            Assert.IsTrue(errors.Count == 1);

            Assert.AreEqual("Target Temperature cannot be below -30C or above 30C", errors[0].ToString());
        }

        [Test]
        public void TestThatBeverageHasInValidTemperatureSetToLowerBoundaryPlusOne()
        {
            mockBev.Temperature = -31;

            var errors = ValidationHelper.Validate(mockBev);

            Assert.IsTrue(errors.Count == 1);
            Assert.AreEqual("Target Temperature cannot be below -30C or above 30C", errors[0].ToString());

        }










        //[Test]
        //public void testTemperatureIsAboveUpperThreshold()
        //{
        //    Beverage mockBev = new Beverage();
        //    mockBev.BeverageID = 2;
        //    mockBev.Temperature = 31;

        //    List<ValidationResult> results = new List<ValidationResult>();

        //    Validator.TryValidateObject(mockBev, new ValidationContext(mockBev), results);

        //    Assert.AreEqual("Target Temperature cannot be below -30C or above 30C", results);
        //}

        //[Test]
        //public void testTemperatureisBelowLowerThreshold()
        //{
        //   Beverage mockBev = new Beverage();
        //   mockBev.BeverageID = 2;
        //   mockBev.Temperature = -31;

        //   List<ValidationResult> results = new List<ValidationResult>();

        //   Validator.TryValidateObject(mockBev, new ValidationContext(mockBev), results);

        //   Assert.AreEqual("Target Temperature cannot be below -30C or above 30C", results);
        //}

    }
}