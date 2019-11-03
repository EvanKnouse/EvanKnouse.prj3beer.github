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

namespace prj3beerAndroid
{
    public class MockDevice : I_Reader
    {
        int[] dummyValues;
        int currentValue;

        public int[] Temps { set => dummyValues = value; }

        public MockDevice(params int[] values)
        {
            this.dummyValues = values;
            this.currentValue = -1;
        }

        public MockDevice()
        {
            this.dummyValues = new int[]{ 1, 3, 5, 7, 9, 11};
        }

        public int GetTemp()
        {
            return dummyValues[++currentValue];
        }

        public void Reset()
        {
            this.currentValue = -1;
        }
    }
}