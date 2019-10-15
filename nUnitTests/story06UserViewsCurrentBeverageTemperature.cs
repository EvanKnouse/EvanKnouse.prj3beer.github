using System;
using System.Collections.Generic;
using System.Text;
using Android.Bluetooth;
using NUnit.Framework;

namespace nUnitTests
{
    [TestFixture]
    class story06UserViewsCurrentBeverageTemperature
    {
        MockBluetooth mockBluetooth;

        private class MockBluetooth
        {
            public int CurrentTemp { get; set; }

            public MockBluetooth()
            {
                this.CurrentTemp = 0;
            }
        }

        public string TemperatureCheck(int nCurrentTemp)
        {
            //does nothing
            return null;
        }

        [SetUp]
        public void Init()
        {
            mockBluetooth = new MockBluetooth();
        }



        [Test]
        public void TestThatTemperatureIsBelowRange()
        {
            string errorMsg = "Temperature Too Low";
            mockBluetooth.CurrentTemp = -31;
            string result = TemperatureCheck(mockBluetooth.CurrentTemp);
            Assert.IsTrue( result.Equals(errorMsg) );

        }

        [Test]
        public void TestThatTemperatureIsAtMinimum()
        {
            string msg = "-30\u00B0C";
            mockBluetooth.CurrentTemp = -30;
            string result = TemperatureCheck(mockBluetooth.CurrentTemp);
            Assert.IsTrue(result.Equals(msg));
        }

        [Test]
        public void TestThatTemperatureBelowZero()
        {
            string msg = "-1\u00B0C";
            mockBluetooth.CurrentTemp = -1;
            string result = TemperatureCheck(mockBluetooth.CurrentTemp);
            Assert.IsTrue(result.Equals(msg));
        }

        [Test]
        public void TestThatTemperatureIsZero()
        {
            string msg = "0\u00B0C";
            mockBluetooth.CurrentTemp = 0;
            string result = TemperatureCheck(mockBluetooth.CurrentTemp);
            Assert.IsTrue(result.Equals(msg));
        }

        [Test]
        public void TestThatTemperatureIsAboveZero()
        {
            string msg = "1\u00B0C";
            mockBluetooth.CurrentTemp = 1;
            string result = TemperatureCheck(mockBluetooth.CurrentTemp);
            Assert.IsTrue(result.Equals(msg));
        }

        [Test]
        public void TestThatTemperatureIsAtMaximum()
        {
            string msg = "30\u00B0C";
            mockBluetooth.CurrentTemp = 30;
            string result = TemperatureCheck(mockBluetooth.CurrentTemp);
            Assert.IsTrue(result.Equals(msg));
        }

        [Test]
        public void TestThatTemperatureIsAboveMaximum()
        {
            string errorMsg = "Temperature Too High";
            mockBluetooth.CurrentTemp = 31;
            string result = TemperatureCheck(mockBluetooth.CurrentTemp);
            Assert.IsTrue(result.Equals(errorMsg));
        }

        [Test]
        public void TestThatDeviceIsDisconnected()
        {
            string msg = "2\u00B0C";
            mockBluetooth.CurrentTemp = 2;
            TemperatureCheck(mockBluetooth.CurrentTemp);
            mockBluetooth = null;
            string result = TemperatureCheck(mockBluetooth.CurrentTemp);
            Assert.IsTrue(result.Equals(msg));
        }

        [Test]
        public void TestThatDeviceIsDisconnectedForExtendedPeriod()
        {
            string errorMsg = "Device connection error.";
            mockBluetooth = null;

            TemperatureCheck(mockBluetooth.CurrentTemp);
            TemperatureCheck(mockBluetooth.CurrentTemp);
            TemperatureCheck(mockBluetooth.CurrentTemp);
            TemperatureCheck(mockBluetooth.CurrentTemp);
            string result = TemperatureCheck(mockBluetooth.CurrentTemp);
            Assert.IsTrue(result.Equals(errorMsg));
        }

        [Test]
        public void TestThatDeviceSendsErrantData()
        {
            string msg = "2\u00B0C";
            mockBluetooth.CurrentTemp = 2;
            TemperatureCheck(mockBluetooth.CurrentTemp);
            mockBluetooth.CurrentTemp = -274;
            string result = TemperatureCheck(mockBluetooth.CurrentTemp);
            Assert.IsTrue(result.Equals(msg));
        }

        [Test]
        public void TestThatDeviceContinuouslySendsErrantData()
        {
            string errorMsg = "Device connection error.";
            mockBluetooth.CurrentTemp = -274;

            TemperatureCheck(mockBluetooth.CurrentTemp);
            TemperatureCheck(mockBluetooth.CurrentTemp);
            TemperatureCheck(mockBluetooth.CurrentTemp);
            TemperatureCheck(mockBluetooth.CurrentTemp);
            string result = TemperatureCheck(mockBluetooth.CurrentTemp);
            Assert.IsTrue(result.Equals(errorMsg));
        }

    }
}
