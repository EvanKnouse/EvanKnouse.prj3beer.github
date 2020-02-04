using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using prj3beer.ViewModels;
using System;
using prj3beer.Services;

namespace prj3beer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsMenu : ContentPage
    {
        //Initialize a container for the notifications sub section
        TableSection subSection;

        //Create a reference to an in range switch cell
        SwitchCell inRangeNotifications;

        //Create a reference to a too hot/too cold switch cell
        SwitchCell tooHotColdNotifications;

        public SettingsMenu()
        {
            InitializeComponent();

            //Create the in range notifications switch cell
            //Create an event handler to handle the toggling of the switch
            inRangeNotifications = new SwitchCell { Text = "In Range" };
            inRangeNotifications.OnChanged += (sender, e) =>
            {
                Models.Settings.InRangeSettings = e.Value;
            };

            //Create the in range notifications switch cell
            //Create an event handler to handle the toggling of the switch
            tooHotColdNotifications = new SwitchCell { Text = "Too Hot/Too Cold" };
            tooHotColdNotifications.OnChanged += (sender, e) =>
            {
                Models.Settings.TooHotColdSettings = e.Value;
            };

            //Instantiate the container
            subSection = new TableSection();
            subSection.Add(inRangeNotifications);
            subSection.Add(tooHotColdNotifications);

            //Set the notification master switch to on by default
            switchNotifications.On = Models.Settings.NotificationSettings;

            //Sets the Label on the switch to match the current temperature setting.
            switchTemp.Text = Models.Settings.TemperatureSettings ? "Celsius" : "Fahrenheit";

            //Check the Settings class to see if set to celsius or fahrenheit
            switchTemp.On = Models.Settings.TemperatureSettings;

            inRangeNotifications.On = Models.Settings.InRangeSettings;

            tooHotColdNotifications.On = Models.Settings.TooHotColdSettings;
        }

        /// <summary>
        /// This method will handle the temperature switch functionality
        /// Allows the app to be changed between displaying in Celsius or Fahrenheit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Temp_Switch_Toggled(object sender, ToggledEventArgs e)
        {
            switchTemp.Text = e.Value ? "Celsius" : "Fahrenheit";

            Models.Settings.TemperatureSettings = e.Value;
        }

        /// <summary>
        /// This method will handle the notification master switch functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Notifications_Switch_Toggled(object sender, ToggledEventArgs e)
        {
            //Set the master notification setting to the switch's status (on or off)(true or false)
            Models.Settings.NotificationSettings = e.Value;

            //Reset the last notification sent to NO_MESSAGE
            Notifications.lastNotification = NotificationType.NO_MESSAGE;

            //If the switch is set to on (true)
            if (e.Value)
            {
                //Show the sub section
                ShowSubSettings();
            }
            else
            {
                //Hide the sub section
                HideSubSettings();
            }
        }

        /// <summary>
        /// This method will hide the notifcations sub section
        /// </summary>
        private void HideSubSettings()
        {
            //Disable the in range notifications
            inRangeNotifications.IsEnabled = false;

            //Disable the too hot/ too cold notifications
            tooHotColdNotifications.IsEnabled = false;

            //Remove the notification sub section from the settings table view
            SettingsTable.Root.Remove(subSection);
        }

        /// <summary>
        /// This method will show the notifications sub section
        /// </summary>
        private void ShowSubSettings()
        {
            //Add the notification subsection 
            SettingsTable.Root.Add(subSection);

            //Enable the in range notifications
            inRangeNotifications.IsEnabled = true;

            //Enable the too hot/ too cold notifications
            tooHotColdNotifications.IsEnabled = true;
        }

        //Closes the settings modal
        async private void Close_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}