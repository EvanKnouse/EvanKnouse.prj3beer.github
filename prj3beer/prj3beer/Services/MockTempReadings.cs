using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace prj3beer.Services
{
    public static class MockTempReadings
    {
        private static double temp;
        private static bool goesDown;
        private static bool repeat = true;

        public static double Temp
        {
            get
            {
                return temp;
            }
            set
            {
                temp = value;
            }
        }

        public static bool GoesDown
        {
            get
            {
                return goesDown;
            }
            set
            {
                goesDown = value;
                repeat = false;
            }
        }


        public static void StartCounting(double newTemp=20.0, bool direction=true)
        {
            Temp = newTemp;
            GoesDown = direction;

            Thread.Sleep(1001);

            repeat = true;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (temp < -35.0)
                {
                    goesDown = false;
                }
                if (temp > 35.0)
                {
                    goesDown = true;
                }
                if(goesDown)
                {
                   temp -= 1.0;
                }
                else
                {
                    temp += 1.0;
                }
                return repeat;
            });
        }
    }
}
