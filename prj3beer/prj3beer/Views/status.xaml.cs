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
        BeerContext context;

        public Status(StatusViewModel svm, BeerContext context, Beverage currentBeverage)
        {
            InitializeComponent();

            statusViewModel = svm;
            this.context = context;
            statusViewModel.isCelsius = true;
            SetupPreference(currentBeverage);

            statusViewModel.Temperature = 6;
            BindingContext = statusViewModel;

            // When you first start up the Status Screen, Disable The Inputs
            EnablePageElements(false);

            // If the current Beverage is set,
            if (preferredBeverage != null)
            {
                EnablePageElements(true);
                //currentMetric.Text = statusViewModel.isCelsius ? "Celsius" : "Fahrenheit";
            }
        }

        private void SetupPreference(Beverage passedInBeverage)
        {
            preferredBeverage = context.Preference.Find(passedInBeverage.BeverageID);

            if(preferredBeverage == null)
            {
                preferredBeverage = new Preference() {BeverageID = passedInBeverage.BeverageID,Temperature = passedInBeverage.Temperature };
               
                context.Preference.Add(preferredBeverage);
            }
            else
            {
                context.Preference.Update(preferredBeverage);
            }
            
        }

        private async void UpdatePreference(BeerContext context)
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (SqliteException) { throw; }
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


        /// <summary>
        /// Method For DEBUG
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ChangeTempMetric(object sender, EventArgs args)
        {
            // debug purposes
            //currentMetric.Text = statusViewModel.isCelsius ? "Celsius" : "Fahrenheit";
        }

        /// <summary>
        /// This method will clear the input field when the Entry Box gains focus
        /// </summary>
        /// <param name="sender">Entry Field</param>
        /// <param name="args"></param>
        private void SelectEntryText(object sender, EventArgs args)
        {
            Entry text = (Entry)sender;

            string cursorPosition = ((StatusViewModel)BindingContext).PreferredTemperatureString;
            // Set the value of the entry to an empty string
            text.Text = "";
            text.Text = cursorPosition;

            text.CursorPosition = 0;
            text.SelectionLength = text.Text.Length;
        }
    }
}