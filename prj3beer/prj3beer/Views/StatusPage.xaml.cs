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
    public partial class StatusPage : ContentPage
    {
        
        int bluetoothValue = 5;
        int targetTempValue = 2;

        public StatusPage()
        { 
            InitializeComponent();
           
        }

       

        async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            ((ToolbarItem)(sender)).IsEnabled = false;

            await Navigation.PushModalAsync(new NavigationPage(new SettingsMenu()));

            ((ToolbarItem)(sender)).IsEnabled = true;
        }

        public void UpdateTempField()
        {

        }

        public void TempReadingType(bool type)
        {
            labelCFCurrentTemperature.Text = "\u00B0" + (type ? "C" : "F");
            labelCFTargetTemperature.Text = "\u00B0" + (type ? "C" : "F");
        }

        protected override void OnAppearing()
        {
            TempReadingType(Models.Settings.TemperatureSettings);

            currentTemperature.Text = Models.Settings.TemperatureSettings ? bluetoothValue + "" : (int)Temperature.CelsiusToFahrenheit(bluetoothValue) + "";
            TemperatureInput.Text = Models.Settings.TemperatureSettings ? targetTempValue + "" : (int)Temperature.CelsiusToFahrenheit(targetTempValue) + "";

        }

    }
}