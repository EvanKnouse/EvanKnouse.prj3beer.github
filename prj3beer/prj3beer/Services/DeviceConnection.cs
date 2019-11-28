using System;
using System.Collections.Generic;
using System.Text;

namespace prj3beer.Services
{
    public class DeviceConnection
    {
        public I_Reader Device { get; set; }
        public DeviceConnection()
        {
            //does less than nothing
        }
        public DeviceConnection(MockDevice md)
        {
            //does nothing
        }
        public string TemperatureCheck()
        {
            throw new ApplicationException("Method not implemented.");
        }
    }
}
