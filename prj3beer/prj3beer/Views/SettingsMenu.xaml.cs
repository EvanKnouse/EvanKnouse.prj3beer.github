using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using prj3beer.ViewModels;
using System;

namespace prj3beer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsMenu : ContentPage
    {   
        public SettingsMenu()
        {
            InitializeComponent();

            //Sets the Label on the switch to match the current temperature setting.
            switchTemp.Text = Models.Settings.TemperatureSettings ? "Celsius" : "Fahrenheit";

            //Check the Settings class to see if set to celsius or fahrenheit
            switchTemp.On = Models.Settings.TemperatureSettings;

            //Set the notification master switch to on by default
            switchNotifications.On = Models.Settings.NotificationSettings;
        }

        //changes temperature display settings in response to the switch 
        private void Temp_Switch_Toggled(object sender, ToggledEventArgs e)
        {
            switchTemp.Text = e.Value ? "Celsius" : "Fahrenheit";

            Models.Settings.TemperatureSettings = e.Value;
        }

        private void Notifications_Switch_Toggled(object sender, ToggledEventArgs e)
        {
            Models.Settings.NotificationSettings = e.Value;
        }

        private void InRange_Switch_Toggled(object sender, ToggledEventArgs e)
        {
            Models.Settings.InRangeSettings = e.Value;
        }

        private void TooHotCold_Switch_Toggled(object sender, ToggledEventArgs e)
        {
            Models.Settings.TooHotColdSettings = e.Value;
        }

        //Closes the settings modal
        async private void Close_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}