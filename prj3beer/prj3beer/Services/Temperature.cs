using System;
using System.Collections.Generic;
using System.Text;

namespace prj3beer.Services
{
    /// <summary>
    /// This class stores a list of temperatures used to calculate the an average temperature reading.
    /// It also contains helper methods for doing Temperature Calculations.
    /// </summary>
    class Temperature
    {
        #region Properties
        // Final Int for Min Temperature Allowed
        private const int MIN_TEMP = -30;
        // Final Int for Max Temperature Allowed
        private const int MAX_TEMP = 30;

        // Getter for the List of stored Temperatures
        internal List<double> temperatures { get; }

        // Boolean for checking if the user has their settings set to Fahrenheit.
        internal bool isCelsius = true;
        #endregion

        /// <summary>
        /// Default constructor for a temperature.
        /// Builds a LIST of Integers, and sets the List to have 5 max values.
        /// </summary>
        public Temperature()
        {
            temperatures = new List<double>();
            temperatures.Capacity = 5;
        }

        /// <summary>
        /// This method will convert a Celsius value into a Fahrenheit Value
        /// </summary>
        /// <param name="tempInCels">Passed in Celsius Temp</param>
        /// <returns>Returns the value in Fahrenheit</returns>
        public double CelsiusToFahrenheit(double tempInCels)
        {
            // Returns the rounded integer of the celsuis to fahrenheit temperature
            return (double)(Math.Round(tempInCels * 1.8 + 32));
        }

        /// <summary>
        /// This method will convert a Fahrenheit value into a Celsius Value
        /// </summary>
        /// <param name="tempInFahren">Passed in Fahrenheit Temp</param>
        /// <returns>Returns the value in Celsius</returns>
        public double FahrenheitToCelsius(double tempInFahren)
        {
            // Returns the rounded integer of the Fahrenheit to Celsius temperature
            return (double)(Math.Round((tempInFahren - 32) * (5.0 / 9.0)));
        }
    }
}