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
    class Temperature
    {
        #region Properties
        private const int MIN_TEMP = -30;
        private const int MAX_TEMP = 30;

        private List<int> temperatures;

        private bool isCelsius = true;
        #endregion


        public Temperature()
        {
            temperatures = new List<int>();
            temperatures.Capacity = 5;
        }

        //private int HexToCelsius(string[] hexArray)
        //{
        //    //TODO: convert a hex value reprenting a temperature into that temperature measured in degrees celsius
        //    return 0;
        //}

        private int CelsiusToFahrenheit(int tempInCels)
        {
            return (int)(Math.Round(tempInCels * 1.8 + 32));
        }

        private int CaclulateCurrentTemp()
        {
            int returnTemp = 0;

            foreach( int temp in this.temperatures)
            {
                returnTemp += temp; 
            }

            if (!isCelsius)
            {
                returnTemp = CelsiusToFahrenheit(returnTemp);
            }

            return returnTemp;

        }

        private void addToTemp(int newTemp)
        {
            if(newTemp <= MAX_TEMP && newTemp >= MIN_TEMP)
            {
                if(temperatures.Count == temperatures.Capacity)
                {
                    temperatures.RemoveAt(0);
                }
                temperatures.Add(newTemp);
            }
        }


        public int TemperatureCheck(string[] deviceTemperature)
        {
            int currentTemp = 0;
            int totalTemp = 0;
            string printOut = "";
            int hexVal1;
            int hexVal2;

            //TODO: call HexToCelsius
            //currentTemp = HexToCelsius(deviceTemperature);

            //TODO: validate temperature value
            if (currentTemp > -30 && currentTemp < 30)
            {
                //TODO: push to temperature queue/list
                this.temperatures.Add(currentTemp);

                //TODO: calculate average temp
                foreach (int temp in this.temperatures)
                {
                    totalTemp += temp;
                }
                totalTemp = totalTemp / this.temperatures.Count;

                if (!isCelsius)
                {
                    totalTemp = CelsiusToFahrenheit(totalTemp);
                }

            }

            //return an appropriately incremented temperature value or relevant error message.
            return totalTemp;
        }
    }
}