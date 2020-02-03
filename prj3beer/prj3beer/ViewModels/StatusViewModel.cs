using System;
using System.ComponentModel;
using Xamarin.Forms;
using prj3beer.Views;
using prj3beer.Services;
using prj3beer.Models;

namespace prj3beer.ViewModels
{
    public class StatusViewModel : INotifyPropertyChanged
    {
        BeerContext context = new BeerContext();

        INotificationHandler nh;
        NotificationType lastNotification = NotificationType.NO_MESSAGE;

        double currentTemp;
        public Beverage currentBeverage;
        public Preference preferredBeverage;

        public BeerContext Context { get { return this.context; } }
        
        // Nullable double value for our temperature
        double? _temperature;

        double _minimum;

        double _maximum;

        /// Boolean for wether or not we are in Celsius or Fahrenheit (Default Celsius)
        public bool IsCelsius { get; set; }

        // string to return, either Degree C or Degree F based on Celsius Value
        public string Scale { get { return IsCelsius ? "\u00B0C" : "\u00B0F"; } }

        // Double for storing Minimum Values for Steppers based on Celsius/Fahrenheit
        public double Minimum { get { return _minimum; } set { _minimum = value; } }

        // Double for storing Maximum Values for Steppers based on Celsius/Fahrenheit
        public double Maximum { get { return _maximum; } set { _maximum = value; } }

        // Double for setting/getting values to/from our backing field (nullable double)
        public double? Temperature
        {
            // Get the value stored in the backing field
            get { return _temperature; }

            // Store the passed in value to the backing field. Do a calculation depending if we are monitoring in Fahrenheit or Celsius
            set {
                try
                {
                    _minimum = IsCelsius ? -30 : -22;
                    _maximum = IsCelsius ? 30 : 86;

                    _temperature = IsCelsius ? value : ((value * 1.8) + 32);
                    
                }
                finally
                {
                    OnPropertyChanged("Temperature");
                }
            }
            //set { _temperature = value; }
        }

        // Returns a string using the stored backing field, needs to be a string to show up in an Entry Field
        public string PreferredTemperatureString
        {
            // Get the temperature from the backing field, if the temperature has a value, return it's rounded value, otherwise return an empty string
            get { return _temperature.HasValue ? Math.Round(_temperature.Value).ToString() : ""; }

            // Set the value from the Entry
            set
            {
                try
                {   // store the temperature from the entry, parsed as a double. 
                    _temperature = double.Parse(value);
                }
                catch
                {   // If the field is empty, set temperature to null
                    _temperature = null;
                }
                finally
                {   
                    OnPropertyChanged("PreferredTemperatureString");
                }
            }
        }

        public double CurrentTemp
        {
            set
            {
                if (currentTemp != value)
                {
                    currentTemp = value;

                    if (PropertyChanged != null)
                    {
                        //If the property has changed, fire an event.
                        //PropertyChanged(this, new PropertyChangedEventArgs("CurrentTemp"));
                        NotificationCheck();
                        OnPropertyChanged("CurrentTemp");
                    }
                }
            }
            get
            {
                return currentTemp;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public StatusViewModel()
        {
            nh = DependencyService.Get<INotificationHandler>();

            currentBeverage = Context.Beverage.Find(1);

            preferredBeverage = Context.Preference.Find(1);
            //preferredBeverage = null; // This is what the previous line SHOULD be doing.

            // If that Preferred beverage did not exist, it will be set to null,
            // So if it is null...
            if (preferredBeverage == null)
            {   // Create a new Preferred Beverage, with copied values from the Passed In Beverage.
                preferredBeverage = new Preference() { BeverageID = currentBeverage.BeverageID, Temperature = currentBeverage.Temperature };
                // Add the beverage to the Context (Database)
                Context.Preference.Add(preferredBeverage);
            }

            //Checks for update temps every second.  Will eventually poll an object associated with a
            //bluetooth reading.  Currently communicates with a class bouncing between -35 and 35 degrees celsius.
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                this.CurrentTemp = MockTempReadings.Temp;
                return true;
            });

        }

        private void NotificationCheck()
        {
            int messageType = Notifications.TryNotification(CurrentTemp, preferredBeverage.Temperature, lastNotification);

            if (messageType > 0 && Settings.NotificationSettings == true) //0 corresponds to type of NO_MESSAGE, thus no notification should be sent
            {
                lastNotification = (NotificationType)messageType;
                nh.SendLocalNotification(Notifications.Title[messageType], Notifications.Body[messageType]);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if(changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}