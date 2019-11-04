using NUnit.Framework;
using prj3beerAndroid;

namespace nUnitTests
{
    public class story03UnitTests
    {
        Beverage mockBev;
        int iDefault = 2;

        [SetUp] //This will act as the test suite initializer
        public void Setup()
        {
            //Create a mock Beverage object to test with
            mockBev = new Beverage("Banquet","Coors", iDefault);
        }

        [Test] //This test will test that the app is able to get temperature of beverage
        public void testAppCanGetBeverageIdealTemp()
        {
            // beverage is created with data from database, idealTemp is the expected value
            mockBev = new Beverage("Banquet", "Coors", iDefault);

            Assert.IsTrue(mockBev.getIdealTemp() == 2);
        }

        [Test] // This test will test the handling of null temperature values from the database
        public void testBeverageTemperatureIsNull()
        {
            // beverage is created with data from database, idealTemp is null from database, mocked with -800
            mockBev = new Beverage("Banquet", "Coors", iDefault);

            Assert.IsTrue(mockBev.getIdealTemp() == 2);
        }

        [Test] // This test will test the upper threshold of allowed temperature values
        public void testIdealTempIsAboveUpperThreshold()
        {
            // beverage is created with data from database, idealTemp is above upper threshold of 30
            mockBev = new Beverage("Banquet", "Coors", 31);

            Assert.IsTrue(mockBev.getIdealTemp() == 4);
        }

        [Test] // This test will test the lower threshold of allowed temperature values
        public void testIdealTempisBelowLowerThreshold()
        {
            // beverage is created with data from database, idealTemp is below lower threshold of -30
            mockBev = new Beverage("Banquet", "Coors", -31);

            Assert.IsTrue(mockBev.getIdealTemp() == 4);
        }
    }
}