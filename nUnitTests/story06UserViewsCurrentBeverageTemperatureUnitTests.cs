using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using prj3beer.Services;

namespace nUnitTests
{
    [TestFixture]
    class story06UserViewsCurrentBeverageTemperature
    {
        //MockBluetooth mockBluetooth;
        //MockDevice mockBluetooth;

        DeviceConnection dc = new DeviceConnection();

        //private class MockBluetooth
        //{
        //    public int CurrentTemp { get; set; }

        //    public MockBluetooth()
        //    {
        //        this.CurrentTemp = 0;
        //    }
        //}

        [SetUp]
        public void Init()
        {
            
            //mockBluetooth = new MockDevice(-31, -30, -1, 0, 1, 30, 31, 2, -274, -274, -274, -274, -274, -274, 2);
        }



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
            MockDevice md = new MockDevice(2,-274,-274,-274,-274);
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
    }
}
