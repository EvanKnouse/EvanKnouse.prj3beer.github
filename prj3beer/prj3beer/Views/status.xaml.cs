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
        bool isCelsius = true;
        //Temperature currentTemp = new Temperature();
       
        public Status()
        {
            InitializeComponent();

            // set the current temperature setting to fahrenheit for testing purposes
            //currentTemp.isCelsius = false;

            /**
             * Get the beverage
              *     Check if an preference already exists for the beverage
              *         if it does display the perferences target temp
              *         else display the beverages target temp
              *  
              */
            // set the currentBeverage attribute to a beverage object returned from a database (eventually)
            currentBeverage = GetBeverageFromLocalStorage();

            // 
            SetTargetTempEntryOnStartUp(currentBeverage);

            stpTemp.Value = currentBeverage.IdealTemp;

            UpdateSteppers();

            currentIdealTemp.Text = currentBeverage.IdealTemp.ToString();
        }

        private Beverage GetBeverageFromLocalStorage()
        {
            // right now we don't have local storage working, so we'll send a hardcoded beverage 
            Beverage mockBev = new Beverage();
            // hardcoded values, will eventually get these values from a table
            mockBev.BevID = 1;
            mockBev.Name = "Miller";
            mockBev.IdealTemp = 2;

            // return the beverage, to be set to the status page's currentBeverage attribute
            return mockBev;
        }

        private void SetTargetTempEntryOnStartUp(Beverage currentBeverage)
        {
            // sets the currenTarget text to the currentBeverage's ideal temp
            // the ideal temperature of the beverage object is set from a value either in the main beverage table or favourites table
            double temp = currentBeverage.IdealTemp;

            // set the entry field text value to the currentBeverage's ideal temperature
            currentTarget.Text = temp.ToString();

            // debug purposes
            currentIdealTemp.Text = currentBeverage.IdealTemp.ToString();
        }

        private void UpdateSteppers()
        {
            // set the minimum and maximum values for the stepper, whether
            double currentTemp = Math.Round(currentBeverage.IdealTemp); // 2
            double minValue = -30;
            double maxValue = 30;

            if(!isCelsius)
            {
                minValue = -30 * 1.8 + 32; // -22
                maxValue = 30 * 1.8 + 32; // 86
                currentTemp = Math.Round(currentTemp * 1.8 + 32); // 36
                //stpTemp.Value = Math.Round(currentBeverage.IdealTemp);
            }
            /*else
            {
                //currentTemp = currentBeverage.IdealTemp;
                stpTemp.Minimum = -30 * 1.8 + 32;
                stpTemp.Maximum = 30 * 1.8 + 32;
                stpTemp.Value = Math.Round(currentBeverage.IdealTemp * 1.8 + 32);
            }*/

            stpTemp.Minimum = minValue;
            stpTemp.Maximum = maxValue;
            stpTemp.Value = currentTemp;

            // debug purposes
            currentIdealTemp.Text = currentBeverage.IdealTemp.ToString();
        }

        private void RegisterTemperatureChange(object sender, EventArgs e)
        {
            double newVal;

            if (string.IsNullOrEmpty(((Entry)sender).Text))
            {
                newVal = isCelsius ? currentBeverage.IdealTemp : ((currentBeverage.IdealTemp * 1.8) + 32);
                ((Entry)sender).Text = newVal.ToString();
            }
            else
            {
                newVal = Convert.ToDouble(((Entry)sender).Text);
            }

            /*if(isCelsius)
            {
                currentBeverage.IdealTemp = newVal;
            }
            else if(!isCelsius)
            {
                currentBeverage.IdealTemp = (newVal - 32) * 5 / 9;
            }*/

            //stpTemp.Value = currentBeverage.IdealTemp;
            stpTemp.Value = newVal;

            // debug purposes
            currentIdealTemp.Text = currentBeverage.IdealTemp.ToString();
        }

        private void StpTemp_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            /**
             * "ideal" implementation
             * 
             * Whenever the stepper value is changed by the steppers or typed in...
             * set the currentTarget Entry to +/- its previous value
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

            double newTemp = e.NewValue;

            if(isCelsius)
            {
                currentBeverage.IdealTemp = e.NewValue;
                //newTemp = ( - 32)
            }
            else
            {
                currentBeverage.IdealTemp = (e.NewValue - 32) * 5 / 9;
            }
            
            currentTarget.Text = Math.Round(e.NewValue).ToString();

            // debug purposes
            currentIdealTemp.Text = currentBeverage.IdealTemp.ToString();
        }

        private void ChangeTempMetric(object sender, EventArgs args)
        {
            isCelsius = isCelsius == true ? false : true;

            // debug purposes
            currentMetric.Text = isCelsius ? "Celsius" : "Fahrenheit";

            UpdateSteppers();

            // debug purposes
            currentIdealTemp.Text = currentBeverage.IdealTemp.ToString();
        }
    }
}