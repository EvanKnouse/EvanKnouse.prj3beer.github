using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace nUnitTests
{
    class story03UnitTests
    {
        MockBeverage mockBev;
        int upperThreshold;
        int lowerThreshold;
        String error;


        //In place of "null", -800 will be used for ints
        int iNull = -800;

        [SetUp]
        public void Setup()
        {
            //Create a mock Beverage object to test with
            mockBev = new MockBeverage("Banquet","Coors", -2);

            //Verify the properties of the mock beverage
            Assert.IsTrue(mockBev.idealTemp == -2);
            Assert.IsTrue(mockBev.name.Equals("Banquet"));
            Assert.IsTrue(mockBev.brand.Equals("Coors"));

            //Set upper threshold
            upperThreshold = 20;

            //Set lower threshold
            lowerThreshold = -10;

            //Set default error message;
            error = " ";

        }

        [Test]
        public void testBeverageTemperatureIsNull()
        {
            Assert.IsTrue(mockBev.idealTemp == iNull);
            Assert.IsTrue(error.Equals("Ideal temperature is not available"));
        }

        [Test]
        public void testIdealTempIsAboveUpperThreshold()
        {
            Assert.IsTrue(mockBev.idealTemp > upperThreshold);
            Assert.IsTrue(error.Equals("Ideal temperature is out of range."));
        }

        [Test]
        public void testIdealTempisBelowLowerThreshold()
        {
            Assert.IsTrue(mockBev.idealTemp < lowerThreshold);
            Assert.IsTrue(error.Equals("Ideal temperature is out of range."));
        }
    }
}
