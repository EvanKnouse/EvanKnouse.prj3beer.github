using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using prj3beer.Services;

namespace prj3beer.Services
{
    class CelsiusToFahrenheitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double temp = (double)value;

            if (temp < -30.0 || temp > 30.0)
            {
                return "Temperature Out Of Range";
            }
            
            return Models.Settings.TemperatureSettings ? temp + "\u00B0C" : Temperature.CelsiusToFahrenheit(temp) + "\u00B0F";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
