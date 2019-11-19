using NUnit.Framework;
using prj3beer.Services;

namespace nUnitTests
{
    [TestFixture]
    class Story01UserViewsCurrentBeverageTemperature
    {
        DeviceConnection dc = new DeviceConnection();

        [Test]
        public void TestThatTemperatureIsBelowRange()
        {
            MockDevice md = new MockDevice(-31);
            dc = new DeviceConnection(md);

            string errorMsg = "Temperature Out Of Range";
            dc.TemperatureCheck();
            dc.TemperatureCheck();
            dc.TemperatureCheck();
            dc.TemperatureCheck();
            string result = dc.TemperatureCheck();
            Assert.IsTrue(result.Equals(errorMsg));

        }

        [Test]
        public void TestThatTemperatureIsAtMinimum()
        {
            MockDevice md = new MockDevice(-30);
            dc = new DeviceConnection(md);

            string msg = "-30\u00B0C";
            string result = dc.TemperatureCheck();
            Assert.IsTrue(result.Equals(msg));
        }

        [Test]
        public void TestThatTemperatureBelowZero()
        {
            MockDevice md = new MockDevice(-1);
            dc = new DeviceConnection(md);

            string msg = "-1\u00B0C";
            string result = dc.TemperatureCheck();
            Assert.IsTrue(result.Equals(msg));
        }

        [Test]
        public void TestThatTemperatureIsZero()
        {
            MockDevice md = new MockDevice(0);
            dc = new DeviceConnection(md);

            string msg = "0\u00B0C";
            //mockBluetooth.CurrentTemp = 0;
            string result = dc.TemperatureCheck();
            Assert.IsTrue(result.Equals(msg));
        }

        [Test]
        public void TestThatTemperatureIsAboveZero()
        {
            MockDevice md = new MockDevice(1);
            dc = new DeviceConnection(md);

            string msg = "1\u00B0C";
            //mockBluetooth.CurrentTemp = 1;
            string result = dc.TemperatureCheck();
            Assert.IsTrue(result.Equals(msg));
        }

        [Test]
        public void TestThatTemperatureIsAtMaximum()
        {
            MockDevice md = new MockDevice(30);
            dc = new DeviceConnection(md);

            string msg = "30\u00B0C";
            string result = dc.TemperatureCheck();
            Assert.IsTrue(result.Equals(msg));
        }

        [Test]
        public void TestThatTemperatureIsAboveMaximum()
        {
            MockDevice md = new MockDevice(31);
            dc = new DeviceConnection(md);

            string errorMsg = "Temperature Out Of Range";
            //mockBluetooth.CurrentTemp = 31;
            dc.TemperatureCheck();
            dc.TemperatureCheck();
            dc.TemperatureCheck();
            dc.TemperatureCheck();
            string result = dc.TemperatureCheck();

            Assert.IsTrue(result.Equals(errorMsg));
        }



        [Test]
        public void TestThatDeviceSendsErrantData()
        {
            MockDevice md = new MockDevice(2, -274, -274, -274, -274);
            dc = new DeviceConnection(md);

            string msg = "2\u00B0C";

            dc.TemperatureCheck();
            dc.TemperatureCheck();
            dc.TemperatureCheck();
            dc.TemperatureCheck();
            string result = dc.TemperatureCheck();
            Assert.IsTrue(result.Equals(msg));
        }

        [Test]
        public void TestThatDeviceContinuouslySendsErrantData()
        {
            MockDevice md = new MockDevice(-274, -274, -274, -274, -274);
            dc = new DeviceConnection(md);

            string errorMsg = "Temperature Out Of Range";

            dc.TemperatureCheck();
            dc.TemperatureCheck();
            dc.TemperatureCheck();
            dc.TemperatureCheck();
            string result = dc.TemperatureCheck();
            Assert.IsTrue(result.Equals(errorMsg));
        }

        [Test]
        public void TestThatDeviceIsDisconnected()
        {
            MockDevice md = new MockDevice(2);
            dc = new DeviceConnection(md);

            string msg = "2\u00B0C";

            dc.TemperatureCheck();
            dc.Device = null;
            string result = dc.TemperatureCheck();
            Assert.IsTrue(result.Equals(msg));
        }

        [Test]
        public void TestThatDeviceIsDisconnectedForExtendedPeriod()
        {
            dc = new DeviceConnection(null);

            string errorMsg = "Device Read Error";
            dc.TemperatureCheck();
            dc.TemperatureCheck();
            dc.TemperatureCheck();
            dc.TemperatureCheck();

            string result = dc.TemperatureCheck();
            Assert.IsTrue(result.Equals(errorMsg));
        }

        [Test]
        public void TestThatAppConvertsPositiveCelsiusTempIntoFahrenheit()
        {
            //target temperature in positive degrees celsius to convert
            double c = 20.0;

            //Expected result of converstion method
            int control = 68;

            //Result of conversion method
            int converted = Temperature.ConvertToFahrenheit(c);

            Assert.AreEqual(control, converted);
        }

        [Test]
        public void TestThatAppConvertsNegativeCelsiusTempIntoFahrenheit()
        {
            //target temperature in negative degrees celsius to convert
            double c = -20.0;

            //Expected result of converstion method
            int control = -4;

            //Result of conversion method
            int converted = Temperature.ConvertToFahrenheit(c);

            Assert.AreEqual(control, converted);
        }

        [Test]
        public void TestThatAppConvertsZeroDegreesCelsiusIntoFahrenheit()
        {
            //target temperature, 0 degrees celsius, to convert
            double c = 0.0;

            //Expected result of converstion method
            int control = 32;

            //Result of conversion method
            int converted = Temperature.ConvertToFahrenheit(c);

            Assert.AreEqual(control, converted);
        }
    }
}