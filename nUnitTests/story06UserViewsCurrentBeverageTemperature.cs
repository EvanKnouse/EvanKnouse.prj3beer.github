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

            }
        }

        [SetUp]
        public void Init()
        {
            mockBluetooth = new MockBluetooth();
        }

        //[TearDown]
        //public void Dispose() { }

        [Test]
        public void TestThatTemperatureIsBelowRange()
        {
            mockBluetooth.CurrentTemp = -41;
        }

        [Test]
        public void TestThatTemperatureIsAtMinimum()
        {

        }

        [Test]
        public void TestThatTemperatureBelowZero()
        {

        }

        [Test]
        public void TestThatTemperatureIsZero()
        {

        }

        [Test]
        public void TestThatTemperatureIsAboveZero()
        {

        }

        [Test]
        public void TestThatTemperatureIsAtMaximum()
        {

        }

        [Test]
        public void TestThatTemperatureIsAboveMaximum()
        {

        }

        [Test]
        public void TestThatDeviceIsDisconnected()
        {

        }

        [Test]
        public void TestThatDeviceIsDisconnectedForExtendedPeriod()
        {

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
