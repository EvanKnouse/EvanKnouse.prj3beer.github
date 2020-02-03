using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace prj3beer.Services
{
    class TargetTempConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double temp = (double)value; //The temperature is passed-in as an object, cast it back to a double

            //Checks if the fahrenheit setting is applied and addends the appropriate units to the unconverted/converted temp
            return Models.Settings.TemperatureSettings ? (double)temp : Temperature.CelsiusToFahrenheit((double)temp);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double temp = double.Parse((string)value);

            return Models.Settings.TemperatureSettings ? temp : Temperature.FahrenheitToCelsius(temp);

        }
    }
}
