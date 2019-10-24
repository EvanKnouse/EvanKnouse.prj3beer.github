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
            //TODO: code this, initialize temperatures list.
        }

        private int HexToCelsius(string hexVal)
        {
            //TODO: convert a hex value reprenting a temperature into that temperature measured in degrees celsius
            return 0;
        }

        private int CelsiusToFahrenheit(int tempInCels)
        {
            //TODO: perform the conversion
            return 0;
        }

        public string TemperatureCheck(string[] temperature)
        {
            //TODO: error checks

            //TODO: call HexToCelsius

            //TODO: validate temperature value

            //TODO: push to temperature queue/list
            
            //TODO: calculate average temp

            //return an appropriately incremented temperature value or relevant error message.
            return "";
        }
    }
}
