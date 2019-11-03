using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using prj3beerAndroid;

namespace nUnitTests
{
    [TestFixture]
    class story06UserViewsCurrentBeverageTemperature
    {
        //MockBluetooth mockBluetooth;
        MockDevice mockBluetooth;

        //private class MockBluetooth
        //{
        //    public int CurrentTemp { get; set; }

        //    public MockBluetooth()
        //    {
        //        this.CurrentTemp = 0;
        //    }
        //}



        public string TemperatureCheck(int nCurrentTemp)
        {
            //does nothing
            return null;
        }

        [SetUp]
        public void Init()
        {
            mockBluetooth = new MockDevice(-31, -30, -1, 0, 1, 30, 31, 2, -274, -274, -274, -274, -274, -274, 2);
        }



        [Test]
        public void TestThatTemperatureIsBelowRange()
        {
            string errorMsg = "Temperature Too Low";
            //mockBluetooth.Temps = new int[]{-31};
            string result = TemperatureCheck(mockBluetooth.GetTemp());
            Assert.IsTrue(result.Equals(errorMsg));

        }

        [Test]
        public void TestThatTemperatureIsAtMinimum()
        {
            string msg = "-30\u00B0C";
            //mockBluetooth.CurrentTemp = -30;
            string result = TemperatureCheck(mockBluetooth.GetTemp());
            Assert.IsTrue(result.Equals(msg));
        }

        [Test]
        public void TestThatTemperatureBelowZero()
        {
            string msg = "-1\u00B0C";
            //mockBluetooth.CurrentTemp = -1;
            string result = TemperatureCheck(mockBluetooth.GetTemp());
            Assert.IsTrue(result.Equals(msg));
        }

        [Test]
        public void TestThatTemperatureIsZero()
        {
            string msg = "0\u00B0C";
            //mockBluetooth.CurrentTemp = 0;
            string result = TemperatureCheck(mockBluetooth.GetTemp());
            Assert.IsTrue(result.Equals(msg));
        }

        [Test]
        public void TestThatTemperatureIsAboveZero()
        {
            string msg = "1\u00B0C";
            //mockBluetooth.CurrentTemp = 1;
            string result = TemperatureCheck(mockBluetooth.GetTemp());
            Assert.IsTrue(result.Equals(msg));
        }

        [Test]
        public void TestThatTemperatureIsAtMaximum()
        {
            string msg = "30\u00B0C";
            //mockBluetooth.CurrentTemp = 30;
            string result = TemperatureCheck(mockBluetooth.GetTemp());
            Assert.IsTrue(result.Equals(msg));
        }

        [Test]
        public void TestThatTemperatureIsAboveMaximum()
        {
            string errorMsg = "Temperature Too High";
            //mockBluetooth.CurrentTemp = 31;
            string result = TemperatureCheck(mockBluetooth.GetTemp());
            Assert.IsTrue(result.Equals(errorMsg));
        }



        [Test]
        public void TestThatDeviceSendsErrantData()
        {
            string msg = "2\u00B0C";
            //mockBluetooth.CurrentTemp = 2;
            TemperatureCheck(mockBluetooth.GetTemp());
            //mockBluetooth.CurrentTemp = -274;
            string result = TemperatureCheck(mockBluetooth.GetTemp());
            Assert.IsTrue(result.Equals(msg));
        }

        [Test]
        public void TestThatDeviceContinuouslySendsErrantData()
        {
            string errorMsg = "Device connection error.";
            //mockBluetooth.CurrentTemp = -274;

            TemperatureCheck(mockBluetooth.GetTemp());
            TemperatureCheck(mockBluetooth.GetTemp());
            TemperatureCheck(mockBluetooth.GetTemp());
            TemperatureCheck(mockBluetooth.GetTemp());
            string result = TemperatureCheck(mockBluetooth.GetTemp());
            Assert.IsTrue(result.Equals(errorMsg));
        }

        [Test]
        public void TestThatDeviceIsDisconnected()
        {
            string msg = "2\u00B0C";
            //mockBluetooth.CurrentTemp = 2;
            TemperatureCheck(mockBluetooth.GetTemp());
            mockBluetooth = null;
            string result = TemperatureCheck(mockBluetooth.GetTemp());
            Assert.IsTrue(result.Equals(msg));
        }

        [Test]
        public void TestThatDeviceIsDisconnectedForExtendedPeriod()
        {
            string errorMsg = "Device connection error.";
            mockBluetooth = null;

            TemperatureCheck(mockBluetooth.GetTemp());
            TemperatureCheck(mockBluetooth.GetTemp());
            TemperatureCheck(mockBluetooth.GetTemp());
            TemperatureCheck(mockBluetooth.GetTemp());
            string result = TemperatureCheck(mockBluetooth.GetTemp());
            Assert.IsTrue(result.Equals(errorMsg));
        }
    }
}
