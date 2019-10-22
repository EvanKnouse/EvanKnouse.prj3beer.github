using System;
using System.Collections.Generic;
using System.Text;

namespace prj3beer.Services
{
    class MockBLE
    {
        private String[][] hexReadings; // Stores multiple arrays of temperature readings in hexadecimal.
        private int incrementer; // increments as numbers are called to ensure the same value isn't called twice in a row.

        public string[] Temp { get => hexReadings[incrementer++]; }

        public MockBLE()
        {
            //UNDONE: populate the hexReadings array with dummy data that will provide data appropriate for every identified testing condition
        }
    }
}
