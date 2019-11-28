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
        Beverage currentBeverage;

        public Status()
        {
            InitializeComponent();

            /**
             * Get the beverage
              *     Check if an preference already exists for the beverage
              *         if it does display the perferences target temp
              *         else display the beverages target temp
              *  
              */
            Beverage currentBeverage = getBeverageFromLocalStorage();
            setTargetTempEntryOnStartUp(currentBeverage);

            StpTemp_ValueChanged(currentBeverage,null);

        }

        private void setTargetTempEntryOnStartUp(Beverage currentBeverage)
        {
            //Should check to see if a preference exists for the currentbeverage

            //Sets the currenTarget text to the currentbeverage's ideal temp
            double temp = currentBeverage.IdealTemp;

            currentTarget.Text = temp.ToString();
        }

        private Beverage getBeverageFromLocalStorage()
        {
            //Right now We dont have local storage working, for now send back a Beverage object

            Beverage mockBev = new Beverage(1, "MGD", "Miller", 2);

            return mockBev;
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

            currentTarget.Text = e.NewValue.ToString();
        }
    }
}
