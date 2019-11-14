using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using prj3beer.Models;

namespace nUnitTests
{
    class story04UserIncrementsDecrementsTemperatureSettings
    {
        [SetUp]
        public void Setup()
        {
            
        }

        #region Story04
        [Test]
        public void TestTargetTemperatureIncrementsBy1()
        {
            
        }

        [Test]
        public void TestTargetTemperatureDecrementsBy1()
        {

        }

        [Test]
        public void TestUserTypesInTargetTemp()
        {

        }

        [Test]
        public void TestTargetTempDoesNotIncrementsPastPlus30()
        {

        }

        [Test]
        public void TestTargetTempDoesNotDecrementsPastNegative30()
        {

        }
        #endregion

        #region Story03
        [Test]
        public void testAppCanGetBeverageIdealTemp()
        {
            
        }

        [Test]
        public void testBeverageTemperatureIsNull()
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