using Microsoft.Data.Sqlite;
using prj3beer.Models;
using prj3beer.Services;
using prj3beer.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prj3beer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Status : ContentPage, INotifyPropertyChanged
    {
        // Mock Beverage Object, (comes from bev select)
        //Beverage currentBeverage = new Beverage { BevID = 1, IdealTemp = 2 };
        Preference preferredBeverage;
        StatusViewModel statusViewModel;
        //BeerContext db;

        public Status(StatusViewModel svm, BeerContext context, Beverage currentBeverage)
        {
            InitializeComponent();

            statusViewModel = svm;

            //EstablishDBConnection();

            SetupPreference(currentBeverage);

            // When you first start up the Status Screen, Disable The Inputs
            EnablePageElements(false);

            // If the current Beverage is set,
            if (preferredBeverage != null)
            {
                //update steppers and enable inputs
                //UpdateSteppers();
                EnablePageElements(true);
            }

            // DEBUG
            //currentIdealTemp.Text = currentBeverage.IdealTemp.ToString();
        }

        private void SetupPreference(Beverage passedInBeverage)
        {
            preferredBeverage = new Preference { beverageID = passedInBeverage.BevID, prefTemp = passedInBeverage.IdealTemp };
        }

        private async void UpdatePreference()
        {
            try
            {
                var db = new BeerContext();

                db.Update(preferredBeverage);

                await db.SaveChangesAsync();
            }
            catch (SqliteException) { throw; }

        }


        /// <summary>
        /// This method will enable or disable all inputs on the screen
        /// </summary>
        /// <param name="enabled">True or False</param>
        private void EnablePageElements(bool enabled)
        {   // WORK ON IT
            statusViewModel = new StatusViewModel();

            //TODO: REPLACE = true with the Boolean From Settings Menu
            statusViewModel.IsCelsius = false;
            // trying to set the status view models to celsius or fahrenheit, no luck
            double temptemp = preferredBeverage.prefTemp;
            statusViewModel.PreferredTemperature = statusViewModel.IsCelsius ? temptemp : (temptemp * 1.8 + 32);

            // Enable/Disable Steppers
            this.TemperatureStepper.IsEnabled = enabled;

            // Enable / Disable Entry
            this.TemperatureInput.IsEnabled = enabled;
        }


        /// <summary>
        /// Method For DEBUG
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ChangeTempMetric(object sender, EventArgs args)
        {
            statusViewModel.IsCelsius = statusViewModel.IsCelsius == true ? false : true;

            // debug purposes
            currentMetric.Text = statusViewModel.IsCelsius ? "Celsius" : "Fahrenheit";
        }

        /// <summary>
        /// This method will clear the input field when the Entry Box gains focus
        /// </summary>
        /// <param name="sender">Entry Field</param>
        /// <param name="args"></param>
        private void SelectEntryText(object sender, EventArgs args)
        {
            string cursorPosition = ((StatusViewModel)BindingContext).PreferredTemperatureString;
            // Set the value of the entry to an empty string
            ((Entry)sender).Text = "";
            ((Entry)sender).Text = cursorPosition;

            ((Entry)sender).CursorPosition = 0;
            ((Entry)sender).SelectionLength = ((Entry)sender).Text.Length;
        }
    }
}