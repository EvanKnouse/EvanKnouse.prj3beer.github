﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        internal List<int> temperatures  { get; }

        // Boolean for checking if the user has their settings set to Fahrenheit.
        internal bool isCelsius = true;
        #endregion

        /// <summary>
        /// Default constructor for a temperature.
        /// Builds a LIST of Integers, and sets the List to have 5 max values.
        /// </summary>
        public Temperature()
        {
            temperatures = new List<int>();
            temperatures.Capacity = 5;
        }

        /// <summary>
        /// This method will convert a Celsius value into a Fahrenheit Value
        /// </summary>
        /// <param name="tempInCels">Passed in Celsius Temp</param>
        /// <returns>Returns the value in Fahrenheit</returns>
        internal int CelsiusToFahrenheit(int tempInCels)
        {   
            // Returns the rounded integer of the celsuis to fahrenheit temperature
            return (int)(Math.Round(tempInCels * 1.8 + 32));
        }

        /// <summary>
        /// This method will iterate through the internal list and return the current average temperature.
        /// </summary>
        /// <returns>Current Average Temperature</returns>
        internal int CaclulateCurrentTemp()
        {
            // initialize variable for returning
            int returnTemp = 0;

            // Foreach Loop, will iterate through every temperature in the List.
            foreach( int temp in this.temperatures)
            {   
                // Will total up all the temps into returnTemp
                returnTemp += temp; 
            }

            // Check to see if the isCelsuis is set to Fahrenheit
            if (!isCelsius)
            {   
                // Set returnTemp to be the result of Celsius to Fahrenheit
                returnTemp = CelsiusToFahrenheit(returnTemp);
            }
            // Return the average of the list
            return returnTemp/temperatures.Count;
        }

        /// <summary>
        /// This method will add a temperature to the LIST if it is between the boundary temperatures.
        /// </summary>
        /// <param name="newTemp">Temperature to add to list</param>
        internal void addToTemp(int newTemp)
        {
            // If the temperature is equal to or less than 30, and equal to or greater than -30,
            if(newTemp <= MAX_TEMP && newTemp >= MIN_TEMP)
            {   
                // If our List is at current capacity (5),
                if(temperatures.Count == temperatures.Capacity)
                {   // Then remove the first element in the list
                    temperatures.RemoveAt(0);
                }
                // Add the new temperature to the list.
                temperatures.Add(newTemp);
            }
        }
    }
}