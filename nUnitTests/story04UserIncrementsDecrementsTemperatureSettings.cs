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

        [SetUp]
        public void Setup()
        {
            
            mockBev = new Beverage("MGD", "Miller", 2);
            Preference preference = new Preference();
          
        }

        #region Story04
        // The target temperature value is incremented by 1
        [Test]
        public void TestTargetTemperatureIncrementsBy1()
        {
            double increment = 1;

            double previousTemp = mockBev.IdealTemp;

            // increment the target temperature
            mockBev.IdealTemp = previousTemp + increment;

            Assert.IsTrue(mockBev.IdealTemp == previousTemp + increment);
        }

        // The target temperature value is decremented by 1
        [Test]
        public void TestTargetTemperatureDecrementsBy1()
        {
            double decrement = -1;

            double previousTemp = mockBev.IdealTemp;

            // decrement the target temperature
            mockBev.IdealTemp = previousTemp + decrement;

            Assert.IsTrue(mockBev.IdealTemp == previousTemp + decrement);
        }

        [Test]
        public void TestUserTypesInTargetTemp()
        {
            double userTemp = 7;

            // set the target temperature
            mockBev.IdealTemp = userTemp;

            Assert.IsTrue(mockBev.IdealTemp == userTemp);
        }

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