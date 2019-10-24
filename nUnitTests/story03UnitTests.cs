﻿using System;
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
        int iNull;

        [SetUp]
        public void Setup()
        {
            //Create a mock Beverage object to test with
            mockBev = new MockBeverage("Banquet","Coors", -2);

            //Set upper threshold
            upperThreshold = 30;

            //Set lower threshold
            lowerThreshold = -30;

            // set mock null for integers
            iNull = -800;
        }

        [Test]
        public void verifyProperties() /* not an actual test */
        {
            //Verify the properties of the mock beverage
            Assert.IsTrue(mockBev.getIdealTemp() == -2);
            Assert.IsTrue(mockBev.name.Equals("Banquet"));
            Assert.IsTrue(mockBev.brand.Equals("Coors"));
        }

        public void testAppCanGetBeverageIdealTemp()
        {
            // beverage is created with data from database, idealTemp is the expected value
            mockBev = new MockBeverage("Banquet", "Coors", 2);

            Assert.IsTrue(mockBev.getIdealTemp() == 2);
        }

        [Test]
        public void testBeverageTemperatureIsNull()
        {
            // beverage is created with data from database, idealTemp is null from database, mocked with -800
            mockBev = new MockBeverage("Banquet", "Coors", iNull);

            Assert.IsTrue(mockBev.getIdealTemp() == 4);
        }

        [Test]
        public void testIdealTempIsAboveUpperThreshold()
        {
            // beverage is created with data from database, idealTemp is above upper threshold of 30
            mockBev = new MockBeverage("Banquet", "Coors", 31);

            Assert.IsTrue(mockBev.getIdealTemp() == 4);
        }

        [Test]
        public void testIdealTempisBelowLowerThreshold()
        {
            // beverage is created with data from database, idealTemp is below lower threshold of -30
            mockBev = new MockBeverage("Banquet", "Coors", -31);

            Assert.IsTrue(mockBev.getIdealTemp() == 4);
        }
    }
}