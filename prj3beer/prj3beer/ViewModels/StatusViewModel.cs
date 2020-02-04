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
        

        double currentTemp;
        public Beverage currentBeverage;
        public Preference preferredBeverage;

        //This boolean will control whether or not the timer
        //responsible for the current temperature mock is on or off
        private static bool timerOn = false;

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
                NotificationCheck();

                if (currentTemp != value)
                {

                    currentTemp = value;

                    if (PropertyChanged != null)
                    {
                        //If the property has changed, fire an event.
                        //PropertyChanged(this, new PropertyChangedEventArgs("CurrentTemp"));
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
            if(!timerOn)
            {
                timerOn = true;
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    this.CurrentTemp = MockTempReadings.Temp;
                    return true;
                });
            }
        }

        /// <summary>
        /// This method will check if a notification needs to be sent,
        /// as well as which notification is allowed to be sent based on 
        /// the switch settings in the settings menu
        /// </summary>
        private void NotificationCheck()
        {
            //This corresponds to one of the messages from the NotificationType Enum
            int messageType = Notifications.TryNotification(CurrentTemp, preferredBeverage.Temperature, Notifications.lastNotification);

            if (messageType > 0 && Settings.NotificationSettings) //0 corresponds to type of NO_MESSAGE, thus no notification should be sent
            {
                //Saves the notification that is going to be sent for comparison later
                Notifications.lastNotification = (NotificationType)messageType;

                //In Range notifications are on
                if (Settings.InRangeSettings)
                {
                    //TooHotCold notifications are on
                    if (Settings.TooHotColdSettings)
                    {
                        //All settings are on
                        nh.SendLocalNotification(Notifications.Title[messageType], Notifications.Body[messageType]);
                    }
                    else
                    {
                        //TooHotCold are off, don't send those (message type 1 and 5)
                        if(messageType != 1 && messageType != 5)
                        {
                            nh.SendLocalNotification(Notifications.Title[messageType], Notifications.Body[messageType]);
                        }
                    }
                }
                else //In Range notifications are off
                {
                    //TooHotCold notifications are on
                    if(Settings.TooHotColdSettings)
                    {
                        //In range settings are off, don't send those (message type 2 and 4)
                        if(messageType != 2 && messageType != 4)
                        {
                            nh.SendLocalNotification(Notifications.Title[messageType], Notifications.Body[messageType]);
                        }
                    }
                    else //TooHotCold notifications are off
                    {
                        //Only Master is on, only send message type 3
                        if(messageType != 1 && messageType != 2 && messageType != 4 && messageType != 5)
                        {
                            nh.SendLocalNotification(Notifications.Title[messageType], Notifications.Body[messageType]);
                        }
                    }
                }

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