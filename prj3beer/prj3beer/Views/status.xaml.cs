using prj3beer.Models;
using prj3beer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prj3beer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Status : ContentPage
    {
        // beverage object, set from database selection or favourites selection
        Beverage currentBeverage;

        // Temp Bool Value
        bool isCelsius = false;
       
        public Status()
        {
            InitializeComponent();

            // When you first start up the Status Screen, Disable The Inputs
            EnablePageElements(false);

            // TODO: Grab Value from Internal Beverage Table OR Favorite Beverage Table
            // set the currentBeverage to a beverage object returned from a database (eventually)
            // currentBeverage = GetBeverageFromLocalStorage();

            // If the current Beverage is set,
            if (currentBeverage != null) {
                //update steppers and enable inputs
                UpdateSteppers();
                EnablePageElements(true);
            }

            // DEBUG
            //currentIdealTemp.Text = currentBeverage.IdealTemp.ToString();
        }

        /// <summary>
        /// This method will enable or disable all inputs on the screen
        /// </summary>
        /// <param name="enabled">True or False</param>
        private void EnablePageElements(bool enabled)
        {
            // Enable/Disable Steppers
            this.TemperatureStepper.IsEnabled = enabled;
            // Enable / Disable Entry
            this.TemperatureInput.IsEnabled = enabled;
        }

        private void GetBeverageFromLocalStorage(object sender, EventArgs args)
        {
            // right now we don't have local storage working, so we'll send a hardcoded beverage 
            Beverage mockBev = new Beverage();
            // hardcoded values, will eventually get these values from a table
            mockBev.BevID = 1;
            mockBev.Name = "Miller";
            mockBev.IdealTemp = 2;

            // return the beverage, to be set to the status page's currentBeverage attribute
            currentBeverage = mockBev;

            // Enable the elements on the page
            EnablePageElements(true);
            // Update the steppers
            UpdateSteppers();
        }

        /// <summary>
        /// This method will update the steppers on the page
        /// </summary>
        private void UpdateSteppers()
        {
            // Set initial variables to celsius values
            double currentTemp = Math.Round(currentBeverage.IdealTemp);
            double minValue = -30;
            double maxValue = 30;

            // If currently set to Fahrenheit,
            if(!isCelsius)
            {   // Update values to be in Fahrenheit
                minValue = -30 * 1.8 + 32;
                maxValue = 30 * 1.8 + 32;
                currentTemp = Math.Round(currentTemp * 1.8 + 32);
            }

            // Set Min/Max/Value for Stepper
            TemperatureStepper.Minimum = minValue;
            TemperatureStepper.Maximum = maxValue;
            TemperatureStepper.Value = currentTemp;

            // DEBUG
            // currentIdealTemp.Text = currentBeverage.IdealTemp.ToString();
        }

        /// <summary>
        /// This method handles a change in the Entry Temperature Input
        /// </summary>
        /// <param name="sender">Entry</param>
        /// <param name="e">Event</param>
        private void RegisterTemperatureChange(object sender, EventArgs e)
        {
            // Declare new Double
            double newVal;

            // Empty String Check for Entry Field - TODO HANDLE THIS BETTER?
            if (string.IsNullOrWhiteSpace(((Entry)sender).Text))
            {   // Depending on if we are currently monitoring Celsius for Fahrenheit, return the current Beverage Temp as Celsius (default) or Fahrenheit 
                newVal = isCelsius ? currentBeverage.IdealTemp : ((currentBeverage.IdealTemp * 1.8) + 32);
                // Set the Entry Field to newVal
                TemperatureInput.Text = Math.Round(newVal).ToString() + "\u00B0" + (isCelsius ? "C" : "F");
            }
            else
            {  // Set newVal to equal the double value of the Entry Field
                newVal = Convert.ToDouble(((Entry)sender).Text);
            }

            //Update the Value used in the Stepper
            TemperatureStepper.Value = newVal;

            // DEBUG
            // currentIdealTemp.Text = currentBeverage.IdealTemp.ToString();
        }

        /// <summary>
        /// This method is responsible for updating the Ideal Temperature of the current Beverage
        /// </summary>
        /// <param name="sender">Stepper</param>
        /// <param name="e">Event</param>
        private void StepperValueChange(object sender, ValueChangedEventArgs e)
        {
            /**
             * "ideal" implementation
             * 
             * Whenever the stepper value is changed by the steppers or typed in...
             * set the TemperatureInput Entry to +/- its previous value
             *      Check for a preference for that beverage
             *          if it exists
             *              update its temperature value
             *          else
             *              Create a new one with the agjusted target temperature
             *       Save it locally
             *       
             * note: right now the typed in values dont register,
             *       maybe on Entry change call this even handler with the new value passed in
             * 
             */

            // If the system is set to use celsius,
            if(isCelsius)
            {   // Update the current Ideal temp to the new Stepper Event Value
                currentBeverage.IdealTemp = e.NewValue;
            }
            // The system is set to use fahrenheit
            else
            {   // Update the current Ideal temp from the current Entry to Celcius conversion
                currentBeverage.IdealTemp = (e.NewValue - 32) * 5 / 9;
            }

            // Update the Entry field to be the New Value
            TemperatureInput.Text = Math.Round(e.NewValue).ToString() + "\u00B0" + (isCelsius ? "C" : "F");

            // DEBUG
            //currentIdealTemp.Text = currentBeverage.IdealTemp.ToString();
        }

        /// <summary>
        /// Method For DEBUG
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ChangeTempMetric(object sender, EventArgs args)
        {
            isCelsius = isCelsius == true ? false : true;

            // debug purposes
            currentMetric.Text = isCelsius ? "Celsius" : "Fahrenheit";

            UpdateSteppers();

            // debug purposes
            //currentIdealTemp.Text = currentBeverage.IdealTemp.ToString();
        }

        /// <summary>
        /// This method will clear the input field when the Entry Box gains focus
        /// </summary>
        /// <param name="sender">Entry Field</param>
        /// <param name="args"></param>
        private void ClearInput(object sender, EventArgs args)
        {
            // Set the value of the entry to an empty string
            ((Entry)sender).Text = "";
        }

        private void RevertInput(object sender, TextChangedEventArgs args)
        {   // Set the Entry text to the Current Value to the current Stepper
            ((Entry)sender).Text = TemperatureStepper.Value.ToString();
            // TODO: Determine what Event Should Handle This!!!
        }
    }
}