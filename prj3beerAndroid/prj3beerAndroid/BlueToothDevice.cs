using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Bluetooth;

namespace prj3beerAndroid
{
    class BlueToothDevice : I_Reader
    {
        string hexTemp;
        BluetoothAdapter btDevice;

        public int GetTemp()
        {
            return ConvertHex(this.hexTemp);
        }

        private int ConvertHex(string hexTemp)
        {
            //Converts raw hex value into integer representing degrees celsius
            return 0;
        }

    }
}