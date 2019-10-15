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

        [SetUp]
        public void Init()
        {
            mockBluetooth = new MockBluetooth();
        }



        [Test]
        public void TestThatTemperatureIsBelowRange()
        {
            string ErrorMsg = "Temperature Too Low";
            mockBluetooth.CurrentTemp = -31;
            Assert.IsTrue( ErrorMsg , TemperatureCheck(mockBluetooth.CurrentTemp));

        }

        [Test]
        public void TestThatTemperatureIsAtMinimum()
        {
            mockBluetooth.CurrentTemp = -30;
            Assert.AreEqual('-30', TemperatureCheck(mockBluetooth.CurrentTemp));
        }

        [Test]
        public void TestThatTemperatureBelowZero()
        {
            mockBluetooth.CurrentTemp = -1;
            Assert.AreEqual(-1, TemperatureCheck(mockBluetooth.CurrentTemp));
        }

        [Test]
        public void TestThatTemperatureIsZero()
        {
            mockBluetooth.CurrentTemp = 0;
            Assert.AreEqual(0, TemperatureCheck(mockBluetooth.CurrentTemp));
        }

        [Test]
        public void TestThatTemperatureIsAboveZero()
        {
            mockBluetooth.CurrentTemp = 1;
            Assert.AreEqual(1, TemperatureCheck(mockBluetooth.CurrentTemp));
        }

        [Test]
        public void TestThatTemperatureIsAtMaximum()
        {
            mockBluetooth.CurrentTemp = 30;
            Assert.AreEqual(30, TemperatureCheck(mockBluetooth.CurrentTemp));
        }

        [Test]
        public void TestThatTemperatureIsAboveMaximum()
        {
            mockBluetooth.CurrentTemp = 31;
            Assert.AreEqual(30, TemperatureCheck(mockBluetooth.CurrentTemp));
        }

        [Test]
        public void TestThatDeviceIsDisconnected()
        {
            mockBluetooth  = null;
            Assert.Equals(mockBluetooth.CurrentTemp, 40);
        }

        [Test]
        public void TestThatDeviceIsDisconnectedForExtendedPeriod()
        {
            mockBluetooth = null;
            Assert.Equals(mockBluetooth.CurrentTemp, 40);
            Assert.Equals(mockBluetooth.CurrentTemp, 40);
            Assert.Equals(mockBluetooth.CurrentTemp, 40);
            Assert.Equals(mockBluetooth.CurrentTemp, 40);
            Assert.AreEqual(mockBluetooth.CurrentTemp, 40);

        }

        [Test]
        public void TestThatDeviceSendsErrantData()
        {

        }

        [Test]
        public void TestThatDeviceIsContinuouslySendsErrantData()
        {

        }

    }
}
