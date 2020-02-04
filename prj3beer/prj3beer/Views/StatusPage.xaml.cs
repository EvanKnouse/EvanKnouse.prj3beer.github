using Microsoft.Data.Sqlite;
using prj3beer.Models;
using prj3beer.Services;
using prj3beer.ViewModels;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prj3beer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatusPage : ContentPage
    {
        static StatusViewModel svm;
        static Beverage currentBeverage;
        static Brand currentBrand;
        static Preference preferredBeverage;

        INotificationHandler nh;
        NotificationType lastNotification = NotificationType.NO_MESSAGE;
        
        //Placeholder for target temperature element, implemented in another story.
        //int targetTempValue = 2;

        public StatusPage()
        { 
            InitializeComponent();

            bool checkIfExists = System.IO.File.Exists("placeholder_can.png");

            #region old Default
            /*
            
            #region Story 04 Code
            // Instantiate new StatusViewModel
            svm = new StatusViewModel();

            // Setup the current Beverage (find it from the Context) -- This will be passed in from a viewmodel/bundle/etc in the future.
            //currentBeverage = new Beverage { BeverageID = 1, Name = "Great Western Radler", Brand = svm.Context.Brands.Find(2), Type = Models.Type.Radler, Temperature = 2 };
            currentBeverage = svm.Context.Beverage.Find(1);
            //svm.Context.Beverage.Find(2);

            // Setup the preference object using the passed in beverage
            SetupPreference();

            // When you first start up the Status Screen, Disable The Inputs (on first launch of application)
            EnablePageElements(false);

            // If the current Beverage is set, (will run if a beverage has been selected)
            if (preferredBeverage != null)
            {   // enable all the elements on the page
                EnablePageElements(true);
            }
            #endregion

            #region Story 16 code
            nh = DependencyService.Get<INotificationHandler>();
            //TODO: Call the compare when a new temperature is gotten from our device API, not on a timer
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                NotificationCheck();

                return true;
            });

            #endregion

            */
            #endregion

            #region Story 07 Code

            beverageName.Text = "No Beverage";
            brandName.Text = "No Brand";
            beverageImage.Source = ImageSource.FromFile("placeholder_can");
            //EnablePageElements(false);

            #endregion
        }

        public StatusPage(int selectedBeverageID)
        {
            InitializeComponent();

            #region Story 04/07 Code
            // Instantiate new StatusViewModel
            svm = new StatusViewModel();

            // Setup the current Beverage (find it from the Context) -- This will be passed in from a viewmodel/bundle/etc in the future.
            //currentBeverage = new Beverage { BeverageID = 1, Name = "Great Western Radler", Brand = svm.Context.Brands.Find(2), Type = Models.Type.Radler, Temperature = 2 };
            currentBeverage = svm.Context.Beverage.Find(selectedBeverageID);
            currentBrand = svm.Context.Brands.Find(currentBeverage.BrandID);
            //svm.Context.Beverage.Find(2);

            // Setup the preference object using the passed in beverage
            SetupPreference(selectedBeverageID);

            // When you first start up the Status Screen, Disable The Inputs (on first launch of application)
            EnablePageElements(false);

            // If the current Beverage is set, (will run if a beverage has been selected)
            if (preferredBeverage != null)
            {   // enable all the elements on the page
                EnablePageElements(true);
            }

            PopulateStatusScreen();
            #endregion

            #region Story 16 code
            nh = DependencyService.Get<INotificationHandler>();
            //TODO: Call the compare when a new temperature is gotten from our device API, not on a timer
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                NotificationCheck();

                return true;
            });

            #endregion

        }

        private void PopulateStatusScreen()
        {

            beverageName.Text = currentBeverage.Name.ToString();
            brandName.Text = currentBrand.Name.ToString();
            if (preferredBeverage.ImageSaved())
            {
                //beverageImage.Source = ImageSource.FromFile(preferredBeverage.ImagePath.ToString());
                beverageImage = preferredBeverage.savedImage;
            }
            else
            {
                beverageImage.Source = ImageSource.FromFile("placeholder_can");
            }
            //beverageImage.WidthRequest = Application.Current.MainPage.Width*0.8;
            //beverageImage.HeightRequest = Application.Current.MainPage.Height * 0.6;
            
        }

        public void UpdateViewModel(object sender, EventArgs args)
        {
            svm.IsCelsius = Settings.TemperatureSettings;
        }

        #region Story 04 Methods
        /// <summary>
        /// This method sets up a Preferred beverage object with the passed in beverage
        /// </summary>
        /// <param name="passedInBeverage">Beverage passed in from other page</param>
        //private void SetupPreference(Beverage passedInBeverage)
        private void SetupPreference()
        {   // Set the page's preferred beverage equal to -> Finding the Beverage in the Database.
            // If the object is found in the database, it will return itself immediately,
            // and attach itself to the context (Database).

            // TODO: Handle Pre-existing Preference Object.
            preferredBeverage = svm.Context.Preference.Find(1);
            //preferredBeverage = null; // This is what the previous line SHOULD be doing.
            
            // If that Preferred beverage did not exist, it will be set to null,
            // So if it is null...
            if (preferredBeverage == null)
            {   // Create a new Preferred Beverage, with copied values from the Passed In Beverage.
                preferredBeverage = new Preference() { BeverageID = currentBeverage.BeverageID, Temperature = currentBeverage.Temperature };
                // Add the beverage to the Context (Database)
                svm.Context.Preference.Add(preferredBeverage);
            }
          
        }

        private void SetupPreference(int bevID)
        {   // Set the page's preferred beverage equal to -> Finding the Beverage in the Database.
            // If the object is found in the database, it will return itself immediately,
            // and attach itself to the context (Database).

            // TODO: Handle Pre-existing Preference Object.
            preferredBeverage = svm.Context.Preference.Find(bevID);
            //preferredBeverage = null; // This is what the previous line SHOULD be doing.

            // If that Preferred beverage did not exist, it will be set to null,
            // So if it is null...
            if (preferredBeverage == null)
            {   // Create a new Preferred Beverage, with copied values from the Passed In Beverage.
                preferredBeverage = new Preference(bevID) { BeverageID =bevID, Temperature = currentBeverage.Temperature };
                // Add the beverage to the Context (Database)
                svm.Context.Preference.Add(preferredBeverage);
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
                preferredBeverage.Temperature = svm.IsCelsius ? svm.Temperature.Value : ((svm.Temperature.Value - 32) / 1.8);
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

            UpdatePreference(svm.Context);
            //this.PrefTemp.Text = preferredBeverage.Temperature.ToString();
        }

        /// <summary>
        /// When the Stepper is changed, update the preference
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TemperatureStepperChanged(object sender, ValueChangedEventArgs e)
        {   // Update the preference object using the Context in the StatusViewModel
            UpdatePreference(svm.Context);
        }
        #endregion
        
        async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            ((ToolbarItem)(sender)).IsEnabled = false;

            await Navigation.PushModalAsync(new NavigationPage(new SettingsMenu()));

            ((ToolbarItem)(sender)).IsEnabled = true;
        }

        /// <summary>
        /// This method is called every time the page is opened.
        /// </summary>
        protected override void OnAppearing()
        {   // Instantiate a new StatusViewModel
            svm = new StatusViewModel();


            if (currentBeverage != null)// So default opening no longer uses a drink that does not exist
            {
                // Set it's Monitored Celsius value to the value from the Settings 
                svm.IsCelsius = Settings.TemperatureSettings;

                // Set the Temperature Stepper to the Max/Minimum possible
                TemperatureStepper.Maximum = 86;
                TemperatureStepper.Minimum = -30;

                // Set the temperature of the StatusViewModel to the current preferred beverage temperature
                svm.Temperature = preferredBeverage.Temperature;

                // is we are currently set to Celsius,
                if (svm.IsCelsius)
                {   // Set the Steppers to Min/Max for Celsius,
                    TemperatureStepper.Minimum = -30;
                    TemperatureStepper.Maximum = 30;
                }
                else
                {   // Otherwise set the Min/Max to Fahrenheit
                    TemperatureStepper.Minimum = -22;
                    TemperatureStepper.Maximum = 86;
                }
                //  Update the binding context to equal the new StatusViewModel
                BindingContext = svm;
            }

            
        }


        #region Story 16 Method
        /// <summary>
        /// Performs a check based on the updated current temperature and the desired drink temperature.  Will send the appropriate notification if necessitated by current conditions.
        /// </summary>
        private void NotificationCheck()
        {
            int messageType = Notifications.TryNotification(svm.CurrentTemp, preferredBeverage.Temperature, lastNotification);

            if (messageType > 0) //0 corresponds to type of NO_MESSAGE, thus no notification should be sent
            {
                lastNotification = (NotificationType)messageType;
                nh.SendLocalNotification(Notifications.Title[messageType], Notifications.Body[messageType]);
            }
        }
        #endregion
    }
}