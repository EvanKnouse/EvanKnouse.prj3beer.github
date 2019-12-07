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
        Preference preferredBeverage;
        StatusViewModel statusViewModel;
        BeerContext context;

        /// <summary>
        /// Status Screen
        /// </summary>
        /// <param name="svm">Status View Model</param>
        /// <param name="context">Database Context</param>
        /// <param name="currentBeverage">Beverage Selected from previous screen</param>
        public Status(StatusViewModel svm, BeerContext context, Beverage currentBeverage)
        {   // Initialize the Status Screen
            InitializeComponent();

            // Set our StatusViewModel to the passed in svm object
            statusViewModel = svm;

            // set this context to the passed in database context
            this.context = context;

            // Set the ViewModel's Celcsuis value to true
            //TODO: Grab This Value From Global Variable
            statusViewModel.isCelsius = false;

            // Call the Setup Preference Method with the passed in beverage.
            SetupPreference(currentBeverage);

            // Set the StatusView's Temperature to the Temperature of the Preferred Beverage object
            statusViewModel.Temperature = preferredBeverage.Temperature;

            // Set this page's Binding Context equal to the Status View Model
            BindingContext = statusViewModel;

            // When you first start up the Status Screen, Disable The Inputs (on first launch of application)
            EnablePageElements(false);

            // If the current Beverage is set, (will run if a beverage has been selected)
            if (preferredBeverage != null)
            {   // enable all the elements on the page
                EnablePageElements(true);
            }
        }

        /// <summary>
        /// This method sets up a Preferred beverage object with the passed in beverage
        /// </summary>
        /// <param name="passedInBeverage">Beverage passed in from other page</param>
        private void SetupPreference(Beverage passedInBeverage)
        {   // Set the page's preferred beverage equal to -> Finding the Beverage in the Database.
            // If the object is found in the database, it will return itself immediately,
            // and attach itself to the context (Database).
            preferredBeverage = context.Preference.Find(passedInBeverage.BeverageID);

            // If that Preferred beverage did not exist, it will be set to null,
            // So if it is null...
            if(preferredBeverage == null)
            {   // Create a new Preferred Beverage, with copied values from the Passed In Beverage.
                preferredBeverage = new Preference() {BeverageID = passedInBeverage.BeverageID,Temperature = passedInBeverage.Temperature };
                // Add the beverage to the Context (Database)
                context.Preference.Add(preferredBeverage);
            }
            else
            {   // Otherwise Call Update on the Context, with the current preferred beverage to allow Save Changes to take place
                context.Preference.Update(preferredBeverage);
            }
        }

        /// <summary>
        /// This method will write changes to the Database for any changes that have happened.
        /// </summary>
        /// <param name="context">Database</param>
        public async void UpdatePreference(BeerContext context)
        {
            try
            {   // Set the Temperature of the Preferred beverage to the StatusViewModel's Temperature,
                // Do a calculation if the temperature is currently set to fahrenheit
                preferredBeverage.Temperature = statusViewModel.isCelsius ? statusViewModel.Temperature.Value : ((statusViewModel.Temperature.Value - 32) / 1.8);
            }
            catch (Exception)
            {
                throw;
            }

            try
            {   // Write Changes to Database when it is not busy.
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

            //DEBUG CODE
            this.PrefTemp.Text = preferredBeverage.Temperature.ToString();
            this.PrefTemp.BindingContext = preferredBeverage.Temperature;
        }


        /// <summary>
        /// Method For DEBUG - changes From C to F and F to C
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
        {   // Store the sender casted as an entry to an Entry Object (to avoid casting repeatedly)
            Entry text = (Entry)sender;

            // This string will get the text from the StatusViewModel's Preferred Temperature String
            string cursorPosition = ((StatusViewModel)BindingContext).PreferredTemperatureString;
            // Set the value of the entry to an empty string
            text.Text = "";
            // Then set the text to the text retreived from the SVM
            text.Text = cursorPosition;

            // Set the cursor position to 0
            text.CursorPosition = 0;
            // Select all of the Text in the Entry
            text.SelectionLength = text.Text.Length;

            UpdatePreference(this.context);
            this.PrefTemp.Text = preferredBeverage.Temperature.ToString();
        }
    }
}