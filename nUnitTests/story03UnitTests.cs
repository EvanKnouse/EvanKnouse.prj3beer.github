using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace nUnitTests
{
    class story03UnitTests
    {
        [SetUp]
        public void Setup()
        {
            //Create a mock Beverage object to test with
            MockBeverage mockBev = new MockBeverage("Banquet","Coors", -2);

            //Verify the properties of the mock beverage
            Assert.IsTrue(mockBev.idealTemp == -2);
            Assert.IsTrue(mockBev.name.Equals("Banquet"));
            Assert.IsTrue(mockBev.brand.Equals("Coors"));
        }

        [Test]
        public void testTargetTemperatureIsDisplayed()
        {
            
        }

        [Test]
        public void testBeverageTemperatureIsNull()
        {

        }

        [Test]
        public void testIdealTempTextBoxIsDisplayedCorrectly()
        {

        }

        [Test]
        public void testIdealTempTextBoxIsDisplayedIncorrectly()
        {

        }

        [Test]
        public void testIdealTempIsBelowUpperThreshold()
        {

        }

        [Test]
        public void testIdealTempisAboveLowerThreshold()
        {

        }
    }
}
