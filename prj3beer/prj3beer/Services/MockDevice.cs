using System;
using System.Collections.Generic;
using System.Text;

namespace prj3beer.Services
{
    /// <summary>
    /// A stand-in class for mocking a connected device.  Provides a sequence of temperature readings.
    /// </summary>
    public class MockDevice : I_Reader
    {
        //The sequence of values to be reported incrementally as the app calls for temperatures
        int[] mockValues;
        //Tracks the current index from which to draw the temperature value
        int index;

        //Allows setting custom temperature sequences after instantiation.  For testing purposes.
        public int[] Temps { set => mockValues = value; }

        /// <summary>
        /// A constructor which allows a custom sequence of values to be assigned.
        /// </summary>
        /// <param name="values">The sequence of temperature values that will be returned when called upon.</param>
        public MockDevice(params int[] values)
        {
            this.mockValues = values;
            Reset();
        }

        /// <summary>
        /// A constructor which establishes a default sequence of values.
        /// </summary>
        public MockDevice() : this(new int[] { 22, 22, 22, 22, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 5, 5, 5, 5 })
        {
        }

        /// <summary>
        /// From I_Reader.
        /// </summary>
        /// <returns>An integer representing a temperature in degrees celsius</returns>
        public int GetTemp()
        {
            // If the end of the sequence has been reached, return to the start
            if (index == mockValues.Length)
            {
                Reset();
            }
            return mockValues[index++];
        }

        /// <summary>
        /// Returns the index marker to the beginning of the array.
        /// </summary>
        public void Reset()
        {
            this.index = 0;
        }
    }

}
