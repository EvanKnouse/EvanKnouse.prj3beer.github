using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using prj3beer.Models;

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
            float increment = 1;

            float previousTemp = mockBev.getIdealTemp();

            // increment the target temperature
            mockBev.SetTargetTemp(previousTemp + increment);

            Assert.IsTrue(mockBev.getIdealTemp() == previousTemp + increment);
        }

        // The target temperature value is decremented by 1
        [Test]
        public void TestTargetTemperatureDecrementsBy1()
        {
            float decrement = -1;

            float previousTemp = mockBev.getIdealTemp();

            // decrement the target temperature
            mockBev.SetTargetTemp(previousTemp + decrement);

            Assert.IsTrue(mockBev.getIdealTemp() == previousTemp + decrement);
        }

        [Test]
        public void TestUserTypesInTargetTemp()
        {
            float userTemp = 7;

            // set the target temperature
            mockBev.SetTargetTemp(userTemp);

            Assert.IsTrue(mockBev.getIdealTemp() == userTemp);
        }

        [Test]
        public void TestTargetTempDoesNotIncrementsPastPlus30()
        {
            float maxTemp = 30;

            mockBev.SetTargetTemp(maxTemp);

            float increment = 1;

            float previousTemp = mockBev.getIdealTemp();

            // increment the target temperature
            mockBev.SetTargetTemp(previousTemp + increment);

            Assert.IsTrue(mockBev.getIdealTemp() == maxTemp);
        }

        [Test]
        public void TestTargetTempDoesNotDecrementsPastNegative30()
        {
            float minTemp = -30;

            mockBev.SetTargetTemp(minTemp);

            float decrement = -1;

            float previousTemp = mockBev.getIdealTemp();

            // decrement the target temperature
            mockBev.SetTargetTemp(previousTemp + decrement);

            Assert.IsTrue(mockBev.getIdealTemp() == minTemp);
        }
        #endregion

        #region Story03
        [Test]
        public void testAppCanGetBeverageIdealTemp()
        {
            
        }

        [Test]
        public void testBeverageTemperatureCannotBeNull()
        {
            
        }

        [Test]
        public void testIdealTempIsAboveUpperThreshold()
        {
            
        }

        [Test]
        public void testIdealTempisBelowLowerThreshold()
        {

        }
        #endregion
    }
}