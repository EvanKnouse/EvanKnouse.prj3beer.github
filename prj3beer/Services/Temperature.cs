using System;
using System.Collections.Generic;
using System.Text;

namespace prj3beer.Services
{
    class Temperature
    {
        private List<int> temperatures;
        private const int MIN_TEMP = -30;
        private const int MAX_TEMP = 30;
        private bool isCelsius = true;

        public Temperature()
        {
            temperatures = new List<int>();
            temperatures.Capacity = 5;
        }

        private int HexToCelsius(string[] hexArray)
        {
            //TODO: convert a hex value reprenting a temperature into that temperature measured in degrees celsius
            return 0;
        }

        private int CelsiusToFahrenheit(int tempInCels)
        {
            return (int)(Math.Round(tempInCels * 1.8 + 32));
        }

        public int TemperatureCheck(string[] deviceTemperature)
        {
            int currentTemp = 0;
            int totalTemp = 0;
            string printOut = "";
            int hexVal1;
            int hexVal2;


           
            //TODO: call HexToCelsius
            currentTemp = HexToCelsius(deviceTemperature);

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
