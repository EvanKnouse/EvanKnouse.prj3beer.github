using System;
using System.Collections.Generic;
using System.Text;

namespace prj3beer.Services
{
    /// <summary>
    /// This class is responsible for collecting and handling temperatures that are
    /// being read in from a connected Device. It performs all error checking and will
    /// be responsible for reporting a string to the UI. 
    /// </summary>
    public class DeviceConnection
    {
        // Initialize Temperature Helper Class
        Temperature temperature;

        #region Properties
        // Variable to keep track of concurrent Read Errors
        private int ReadError { get; set; }

        // Variable to keep track of concurrent Out of Range Errors
        private int RangeError { get; set; }

        // Variable to store / return to the screen
        private string ReturnString { get; set; }

        // Getter Setter for a Device
        public I_Reader Device { get; set; }
        #endregion


        /// <summary>
        /// Default Constructor for a Device Connection
        /// Sets Errors to Zero, and initializes a default Message.
        /// Creates new instances of a Device and Temperature
        /// </summary>
        public DeviceConnection()
        {
            ReadError = 0;
            RangeError = 0;
            ReturnString = "Waiting for device";

            Device = new MockDevice();
            temperature = new Temperature();
        }

        /// <summary>
        /// Same as above, but with a passed in device
        /// </summary>
        /// <param name="md">Passed In Device</param>
        public DeviceConnection(I_Reader md):this()
        {
            Device = md;
        }

        /// <summary>
        /// The main method for calculating and setting the String to be returned.
        /// </summary>
        /// <returns></returns>
        public string TemperatureCheck()
        {
            // Check for a null Device
            if(this.Device == null)
            {   // Increase the Read Error by 1
                ReadError++;
            }
            // If the device is not null,
            else
            {   // Get the current temperature from the Device
                int currentTemp = Device.GetTemp();
                // Setup a variable to keep track of the total temperature
                int totalTemp = 0;

                // Boundary Check, see if the temperature is within operating range
                if (currentTemp >= -30 && currentTemp <= 30)
                {   // If the Temperature is successfully read, reset the error count.
                    ReadError = 0;
                    RangeError = 0;

                    // Add the current temperature to the Array of Temperatures used for averaging
                    temperature.addToTemp(currentTemp);

                    // Perform a calculation
                    totalTemp = temperature.CaclulateCurrentTemp();

                    // Check if the user has their temperature set to Fahrenheit
                    if (!temperature.isCelsius)
                    {   // Perform the conversion to Fahrenheit
                        totalTemp = temperature.CelsiusToFahrenheit(totalTemp);
                        // Append Degrees F to the end of the string. 
                        ReturnString = totalTemp + "\u00B0F";
                    }
                    // User has their temperature setting set to Celsius 
                    else
                    {   // Append Degrees C to the end of the string. 
                        ReturnString = totalTemp + "\u00B0C";
                    }
                }
                // If the Curren Temperature is Above/Below +/- 30
                else
                {   // Increase the Range Error
                    RangeError++;
                }
            }
            // If there is 5 concurrent Read Errors,
            if (ReadError == 5)
            {   // Set the String to be Device Read Error
                ReturnString = "Device Read Error";
            }
            // If there is 5 concurrent Out of Range Errors,
            if (RangeError == 5)
            {   // Set the String to be Temperature Out Of Range
                ReturnString = "Temperature Out Of Range";
            }
            // Return the final string (for textbox Printout)
            return ReturnString;
        }
    }
    
}
