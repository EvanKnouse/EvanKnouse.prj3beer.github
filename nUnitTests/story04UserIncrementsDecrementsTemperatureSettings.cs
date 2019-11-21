using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using prj3beer.Models;
using System.ComponentModel.DataAnnotations;

namespace nUnitTests
{
    class story04UserIncrementsDecrementsTemperatureSettings
    {
        Beverage mockBev;

        // Setup the Beverage object for testing
        [SetUp]
        public void Setup()
        {
            mockBev = new Beverage("MGD", "Miller", 2);
            Preference preference = new Preference();
        }

        #region Story04
        // Test the target temperature value is incremented by 1
        [Test]
        public void TestTargetTemperatureIncrementsBy1()
        {
            double increment = 1;

            double previousTemp = mockBev.IdealTemp;

            // increment the target temperature
            mockBev.IdealTemp = previousTemp + increment;

            Assert.IsTrue(mockBev.IdealTemp == previousTemp + increment);
        }

        // Test the target temperature value is decremented by 1
        [Test]
        public void TestTargetTemperatureDecrementsBy1()
        {
            double decrement = -1;

            double previousTemp = mockBev.IdealTemp;

            // decrement the target temperature
            mockBev.IdealTemp = previousTemp + decrement;

            Assert.IsTrue(mockBev.IdealTemp == previousTemp + decrement);
        }

        // Test the target temperature value does not increment past 30
        [Test]
        public void TestTargetTempDoesNotIncrementsPastPlus30()
        {
            double maxTemp = 30;

            mockBev.IdealTemp = maxTemp;

            double increment = 1;

            double previousTemp = mockBev.IdealTemp;

            // increment the target temperature
            mockBev.IdealTemp = previousTemp + increment;

            Assert.IsTrue(mockBev.IdealTemp == maxTemp);
        }

        // Test the target temperature value does not decrement past -30
        [Test]
        public void TestTargetTempDoesNotDecrementsPastNegative30()
        {
            double minTemp = -30;

            mockBev.IdealTemp = minTemp;

            double decrement = -1;

            double previousTemp = mockBev.IdealTemp;

            // decrement the target temperature
            mockBev.IdealTemp = previousTemp + decrement;

            Assert.IsTrue(mockBev.IdealTemp == minTemp);
        }

        #region User Input
        // Test the target temperature value is set to the valid user input value
        // and the Beverage object does not send an error
        [Test]
        public void TestUserTypesInValidTargetTemp()
        {
            double userTemp = 7;

            // set the target temperature
            mockBev.IdealTemp = userTemp;

            var errors = ValidationHelper.Validate(mockBev);

            Assert.IsTrue(errors.Count == 0);

            Assert.IsTrue(mockBev.IdealTemp == userTemp);
        }

        // Test the target temperature value is not set to the high invalid user input value
        // and the Beverage object sends an error
        [Test]
        public void TestUserTypesInInvalidHighTargetTemp()
        {
            //mockBev.IdealTemp = 2;

            double userTemp = 31;

            // set the target temperature
            mockBev.IdealTemp = userTemp;

            var errors = ValidationHelper.Validate(mockBev);

            Assert.AreEqual("Target Temperature cannot be below -30C or above 30C", errors[0].ErrorMessage);

            //Assert.IsTrue(mockBev.IdealTemp == 2);
        }

        // Test the target temperature value is not set to the low invalid user input value
        // and the Beverage object sends an error
        [Test]
        public void TestUserTypesInInvalidLowTargetTemp()
        {
            //mockBev.IdealTemp = 2;

            double userTemp = -31;

            // set the target temperature
            mockBev.IdealTemp = userTemp;

            var errors = ValidationHelper.Validate(mockBev);

            Assert.AreEqual("Target Temperature cannot be below -30C or above 30C", errors[0].ErrorMessage);

            //Assert.IsTrue(mockBev.IdealTemp == 2);
        }
        #endregion
        #endregion

        #region Story03
        [Test]
        public void testAppCanGetBeverageIdealTemp()
        {
            double idealTemp = 2;

            Assert.IsTrue(mockBev.IdealTemp == idealTemp);
        }

        [Test]
        public void testBeverageTemperatureCannotBeNull()
        {
            double? noValue = null;

           // mockBev.SetTargetTemp();

            //Assert.IsFalse(mockBev.IdealTemp != null);
            
        }

        [Test]
        public void testIdealTempIsAboveUpperThreshold()
        {
            Assert.AreEqual("Target Temperature cannot be below -30C or above 30C", mockBev = new Beverage("Banquet", "Coors", 31));
        }

        [Test]
        public void testIdealTempisBelowLowerThreshold()
        {
           mockBev = new Beverage("Banquet", "Coors", -31);

           List<ValidationResult> results = new List<ValidationResult>();

           Validator.TryValidateObject(mockBev, new ValidationContext(mockBev), results);

           Assert.AreEqual("Target Temperature cannot be below -30C or above 30C", results);
        }
        #endregion
    }
}