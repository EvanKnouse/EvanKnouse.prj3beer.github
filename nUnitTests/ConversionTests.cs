using NUnit.Framework;
using prj3beer.Services;

namespace nUnitTests
{
    [TestFixture]
    class ConversionTests
    {
        [Test]
        public void TestThatAppConvertsNegativeCelsiusTempIntoFahrenheit()
        {
            //target temperature in negative degrees celsius to convert
            double c = -20.0;

            //Expected result of converstion method
            string control = (-4).ToString() + "\u00B0F";

            //Result of conversion method
            string converted = Temperature.ConvertToFahrenheit(c);

            Assert.AreEqual(control, converted);
        }

        [Test]
        public void TestThatAppConvertsZeroDegreesCelsiusIntoFahrenheit()
        {
            //target temperature, 0 degrees celsius, to convert
            double c = 0.0;

            //Expected result of converstion method
            string control = (32).ToString() + "\u00B0F";

            //Result of conversion method
            string converted = Temperature.ConvertToFahrenheit(c);

            Assert.AreEqual(control, converted);
        }

        [Test]
        public void TestThatAppConvertsPositiveCelsiusTempIntoFahrenheit()
        {
            //target temperature in positive degrees celsius to convert
            double c = 20.0;

            //Expected result of converstion method
            string control = (68).ToString() + "\u00B0F";

            //Result of conversion method
            string converted = Temperature.ConvertToFahrenheit(c);

            Assert.AreEqual(control, converted);
        }
    }
}
