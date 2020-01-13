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
        static Brand GWBbrand = new Brand() { brandID = 1, brandName = "Great Western Brewing Company" };
        Beverage GreatWestRadler = new Beverage { BeverageID = 1, Brand = GWBbrand, Name = "Great Western Radler", Temperature = 3, Type = Type.Radler };

        //Create a container for validation results (error messages)
        IList<ValidationResult> errors;
        #endregion

        [Test]
        public void TestThatMinBoundaryBeverageIDIsValid()
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void TestThatBelowMinBoundaryBeverageIDIsInvalid()
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void TestThatMaxBoundaryBeverageNameIsValid()
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void TestThatMaxBoundaryBrandNameIsValid()
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void TestThatMinBoundaryBeverageNameIsValid()
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void TestThatMinBoundaryBrandNameIsValid()
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void TestThatBelowMinBeverageNameIsInvalid()
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void TestThatAboveMaxBeverageNameIsInvalid()
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void TestThatBelowMinBrandNameIsInvalid()
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void TestThatAboveMaxBrandNameIsInvalid()
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void TestThatBelowMinBeverageIDIsInvalid()
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void TestThatBeverageTypeIsValid()
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void TestThatBeverageTypeIsInvalid()
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void TestThatMinTemperatureIsValid()
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void TestThatMaxTemperatureIsValid()
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void TestThatBelowMinTemperatureIsInvalid()
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void TestThatAboveMaxTemperatureIsInvalid()
        {
            Assert.IsTrue(false);
        }
    }
}